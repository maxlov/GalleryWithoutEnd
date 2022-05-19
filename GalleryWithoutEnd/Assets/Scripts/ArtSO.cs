using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ArtSO", menuName = "GallerySystem/ArtSO")]
public class ArtSO : ScriptableObject
{
    // Holds data used by art frame to display art and art info.
    public string Title = "Untitled";
    public string Artist = "Unknown";
    public string Description = "";
    public Texture2D Art;
}
