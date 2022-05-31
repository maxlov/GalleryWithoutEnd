using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ArtSO", menuName = "GallerySystem/ArtSO")]
public class ArtSO : ScriptableObject
{
    public string Title = "Untitled";
    public string Artist = "Unknown";
    public string Description = "";
    public int ID;
    public Texture2D Art;

    public UnityEvent ReceivedData = new UnityEvent();

    private void OnDisable()
    {
        ReceivedData.RemoveAllListeners();
    }
}
