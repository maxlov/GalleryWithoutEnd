using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ArtSO", menuName = "GallerySystem/ArtSO")]
public class ArtSO : ScriptableObject
{
    public string Title = "Untitled";
    public string Artist = "Unknown";
    public string Description = "";
    public Texture2D Art;
}
