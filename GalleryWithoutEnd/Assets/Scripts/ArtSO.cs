using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ArtSO", menuName = "GallerySystem/ArtSO")]
public class ArtSO : ScriptableObject
{
    public string title = "";
    public string artist = "";
    public string details = "";
    public RawImage art;
}
