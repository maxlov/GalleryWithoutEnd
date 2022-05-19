using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using HamApi.ObjectInfo;

public class HamApiManager : MonoBehaviour, IApiManager
{
    [SerializeField]
    private string ApiKey;

    private Root data;
    private Texture2D artTexture;

    private string artUrl;

    public IEnumerator RequestRandomArt(ArtSO artPiece)
    {
        int artNumber = Random.Range(1, 239689);
        artUrl = $"https://api.harvardartmuseums.org/object?size=1&page={artNumber}&apikey={ApiKey}";

        Debug.Log(artUrl);

        yield return RequestArtData("https://api.harvardartmuseums.org/object?size=1&page=39354&apikey=2ec9806e-146f-4c71-9b2d-c10b0411bf15");

        string artImageUrl = data.records[0].images[0].baseimageurl;

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
