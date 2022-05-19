using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using MetMuseumApi.Endpoints.Object;

namespace MetMuseumApi
{
    public class MetApiManager : MonoBehaviour, IApiManager
    {
        private Root data;
        private Texture2D artTexture;
        private string artUrl;

        public IEnumerator RequestRandomArt(ArtSO artPiece)
        {
            while (true)
            {
                int artNumber = Random.Range(1, 471581);
                artUrl = $"https://collectionapi.metmuseum.org/public/collection/v1/objects/{artNumber}";

                Debug.Log(artUrl);

                yield return RequestArtData(artUrl);

                if (data.isPublicDomain)
                    break;
                else
                    yield return new WaitForSeconds(.5f);
            }

            string artImageUrl = data.primaryImage;

            yield return RequestArtImage(artImageUrl);

            ArtObjectSetup(artPiece, data, artTexture);
        }

        IEnumerator RequestArtData(string url)
        {
            UnityWebRequest artRequest = UnityWebRequest.Get(url);

            yield return artRequest.SendWebRequest();

            if (artRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(artRequest.error);
                yield break;
            }

            data = JsonConvert.DeserializeObject<Root>(artRequest.downloadHandler.text);
            Debug.Log("Deserialized data");
        }

        IEnumerator RequestArtImage(string url)
        {
            UnityWebRequest artImageRequest = UnityWebRequestTexture.GetTexture(url);

            yield return artImageRequest.SendWebRequest();

            if (artImageRequest.result == UnityWebRequest.Result.ConnectionError || artImageRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(artImageRequest.error);
                yield break;
            }

            artTexture = DownloadHandlerTexture.GetContent(artImageRequest);
        }

        private void ArtObjectSetup(ArtSO artPiece, Root data, Texture2D artTexture)
        {
            artPiece.Title = data.title;
            artPiece.Artist = data.artistDisplayName;
            artPiece.Description = data.artistDisplayBio;
            artPiece.Art = artTexture;
            artPiece.Art.filterMode = FilterMode.Trilinear;
        }
    }
}