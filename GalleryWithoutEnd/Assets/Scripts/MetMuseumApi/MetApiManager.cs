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
        private string artDataUrl;

        [SerializeField] private string searchTerm = "";

        const string baseApiUrl = "https://collectionapi.metmuseum.org/public/collection/v1";

        private CoroutineQueue queue;

        private void Awake()
        {
            // Setup coroutine queue to allow only one coroutine at a time
            queue = new CoroutineQueue(1, StartCoroutine);
        }

        public void QueueArtRequest(ArtSO artPiece)
        {
            queue.Run(ArtRequest(artPiece));
        }

        public IEnumerator ArtRequest(ArtSO artPiece)
        {
            if (searchTerm != string.Empty)
                yield return SearchDataRequest();
            else
                yield return RandomDataRequest();

            Debug.Log(artDataUrl);

            // Download art URL to artTexture
            yield return RequestArtImage(data.primaryImage);

            // Setup art scriptable object
            ArtObjectSetup(artPiece, data, artTexture);
        }

        IEnumerator RandomDataRequest()
        {
            for (int loopCount = 0; ; loopCount++)
            {
                int objectId = Random.Range(1, 471581);
                artDataUrl = baseApiUrl + "/objects/" + objectId;

                yield return RequestArtData(artDataUrl);

                if (data.isPublicDomain)
                    break;

                // Slow down loop if script making too many requests
                if (loopCount % 40 == 0)
                    yield return new WaitForSeconds(1f);
            }
        }

        IEnumerator SearchDataRequest()
        {
            string searchUrl = baseApiUrl + "/search?hasImages=true&" + searchTerm;

            yield return RequestArtSearch(searchUrl);

            if (searchData.total <= 0)
            {
                Debug.LogError("No objects found with query");
                yield break;
            }

            for (int loopCount = 0; loopCount <= 80; loopCount++)
            {
                int objectId = searchData.objectIDs[Random.Range(0, searchData.total + 1)];
                artDataUrl = baseApiUrl + "/objects/" + objectId;

                yield return RequestArtData(artDataUrl);

                if (data.isPublicDomain)
                    break;

                // Slow down loop if script making too many requests
                if (loopCount % 40 == 0)
                    yield return new WaitForSeconds(1f);
            }

            // Check for no public domain art in query
            if (data.primaryImage == string.Empty)
            {
                Debug.LogError("No public domain objects found in query");
                yield break;
            }

            yield return RequestArtData(artDataUrl);
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

            artPiece.ID = data.objectID;

            artPiece.ReceivedData.Invoke();
        }
    }
}