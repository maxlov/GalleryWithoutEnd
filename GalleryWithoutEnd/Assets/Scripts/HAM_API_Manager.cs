using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

[CreateAssetMenu(fileName = "HAM_API_Manager", menuName = "GallerySystem/HAM API Manager")]
public class HAM_API_Manager : ScriptableObject
{
    [SerializeField]
    private string API_key;

    public ArtSO GrabRandomArt()
    {
        string url = "";
        UnityWebRequest art_request = UnityWebRequest.Get(url);



        ArtSO art_piece = ScriptableObject.Instantiate<ArtSO>(new ArtSO());

        return art_piece;
    }
}
