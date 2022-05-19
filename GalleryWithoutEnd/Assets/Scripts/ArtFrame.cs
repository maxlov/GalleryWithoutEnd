using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtFrame : MonoBehaviour
{
    [SerializeField] HamApiManager ArtManager;

    [SerializeField] private Image artwork;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI artist;
    [SerializeField] private TextMeshProUGUI description;

    private ArtSO ArtPiece;

    private void Start()
    {
        ArtPiece = ScriptableObject.CreateInstance<ArtSO>();
    }

    public void GetArtFromManager()
    {
        StartCoroutine(SetupArtPiece());
    }

    IEnumerator SetupArtPiece()
    {
        yield return ArtManager.RequestRandomArt(ArtPiece);
        UpdateUI();
    }

    void UpdateUI()
    {
        artwork.enabled = false;
        artwork.material.mainTexture = ArtPiece.Art;
        artwork.rectTransform.sizeDelta = new Vector2(ArtPiece.Art.width, ArtPiece.Art.height);
        artwork.enabled = true;

        title.text = ArtPiece.Title;
        artist.text = ArtPiece.Artist;
        description.text = ArtPiece.Description;
    }
}
