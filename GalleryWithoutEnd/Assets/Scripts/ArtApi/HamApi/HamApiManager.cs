using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using HamApi.ObjectInfo;

public class HamApiManager : BaseApiManager
{
    [SerializeField]
    private string ApiKey;

    private Root data;
    private Texture2D artTexture;

    private string artUrl;

    private CoroutineQueue queue;
    protected override void Awake()
    {
        base.Awake();

        // Setup coroutine queue to allow only one coroutine at a time
        queue = new CoroutineQueue(1, StartCoroutine);
    }

    public override void QueueArtRequest(ArtSO artPiece)
    {
        queue.Run(ArtRequest(artPiece));
    }

    public IEnumerator ArtRequest(ArtSO artPiece)
    {
        // Generate random number for art URL
        int artNumber = Random.Range(1, 239689);
        artUrl = $"https://api.harvardartmuseums.org/object?size=1&page={artNumber}&apikey={ApiKey}";

        Debug.Log(artUrl);

        // Get art data
        yield return RequestArtData("https://api.harvardartmuseums.org/object?size=1&page=39354&apikey=2ec9806e-146f-4c71-9b2d-c10b0411bf15");

        // Snag URL for art image
        string artImageUrl = data.records[0].images[0].baseimageurl;

        // Download art image to artTexture
        yield return RequestArtImage(artImageUrl);

        // Setup art object
        ArtObjectSetup(artPiece, data, artTexture);
    }

    IEnumerator RequestArtData(string url)
    {
        // Pulls Json data into a custom class object
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
        // Downloads url as texture
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
        // Sets up art object with data

        if (data.records[0].titlescount > 0)
            artPiece.Title = data.records[0].title;

        if (data.records[0].peoplecount > 0)
            artPiece.Artist = data.records[0].people[0].displayname;

        if (data.records[0].description != null)
            artPiece.Description = data.records[0].description.ToString();

        artPiece.Art = artTexture;
        artPiece.Art.filterMode = FilterMode.Trilinear;
    }
}
