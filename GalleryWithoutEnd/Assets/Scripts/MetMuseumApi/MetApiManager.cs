using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Item = MetMuseumApi.Endpoints.Item;
using Search = MetMuseumApi.Endpoints.Search;

namespace MetMuseumApi
{
    public class MetApiManager : MonoBehaviour, IApiManager
    {
        private Search.Root searchData;
        private Item.Root data;
        private Texture2D artTexture;
        private string artUrl;

        [SerializeField] private string searchTerm = "";

        public IEnumerator RequestRandomArt(ArtSO artPiece)
        {
            string searchUrl = "https://collectionapi.metmuseum.org/public/collection/v1/search?hasImages=true&";

            // Create search term with query

            if (searchTerm != string.Empty)
            {
                searchUrl += searchTerm;

                yield return RequestArtSearch(searchUrl);

                if (searchData.total <= 0)
                {
                    Debug.LogError("No objects found with query");
                    yield break;
                }

                int loopCount = 0;
                while (true)
                {
                    int objectId = searchData.objectIDs[Random.Range(0, searchData.total + 1)];
                    artUrl = $"https://collectionapi.metmuseum.org/public/collection/v1/objects/{objectId}";

                    Debug.Log(artUrl);

                    yield return RequestArtData(artUrl);


                    // No public domain art in query
                    if (loopCount >= 80)
                    {
                        Debug.LogError("No public domain objects found in query");
                        break;
                    }

                    // Public art succesfully found
                    if (data.isPublicDomain)
                        break;

                    // Slow down loop if script making too many requests
                    if (loopCount++ % 40 == 0)
                        yield return new WaitForSeconds(2f);
                }
                // If check here if data is not null

                yield return RequestArtData(artUrl);
            }
            else
            {
                int loopCount = 0;
                while (true)
                {
                    int artNumber = Random.Range(1, 471581);
                    artUrl = $"https://collectionapi.metmuseum.org/public/collection/v1/objects/{artNumber}";

                    Debug.Log(artUrl);

                    yield return RequestArtData(artUrl);

                    if (data.isPublicDomain)
                        break;

                    // Slow down loop if script making too many requests
                    if (loopCount++ % 40 == 0)
                        yield return new WaitForSeconds(1f);
                }
            }

            // Get art URL from data
            string artImageUrl = data.primaryImage;

            // Download art URL to artTexture
            yield return RequestArtImage(artImageUrl);

            // Setup art scriptable object
            ArtObjectSetup(artPiece, data, artTexture);
        }

        IEnumerator RequestArtSearch(string url)
        {
            UnityWebRequest searchRequest = UnityWebRequest.Get(url);

            yield return searchRequest.SendWebRequest();

            if (searchRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(searchRequest.error);
                yield break;
            }

            searchData = JsonConvert.DeserializeObject<Search.Root>(searchRequest.downloadHandler.text);
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

            data = JsonConvert.DeserializeObject<Item.Root>(artRequest.downloadHandler.text);
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

        private void ArtObjectSetup(ArtSO artPiece, Item.Root data, Texture2D artTexture)
        {
            artPiece.Title = data.title;
            artPiece.Artist = data.artistDisplayName;
            artPiece.Description = data.artistDisplayBio;
            artPiece.Art = artTexture;
            artPiece.Art.filterMode = FilterMode.Trilinear;
        }
    }
}