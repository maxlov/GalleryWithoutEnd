using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtFrame : MonoBehaviour
{
    [SerializeField] private GameObject ArtManager;
    private IApiManager ArtManagerInterface;

    [SerializeField] private Image artwork;
    [SerializeField] private AspectRatioFitter aspectFitter;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI artist;
    [SerializeField] private TextMeshProUGUI description;

    private ArtSO ArtPiece;

    private void Start()
    {
        ArtPiece = ScriptableObject.CreateInstance<ArtSO>();
        if (!ArtManager.TryGetComponent<IApiManager>(out ArtManagerInterface))
            Debug.LogError("No IApiManager component found on ArtManager");
    }

    public void GetArtFromManager()
    {
        StartCoroutine(SetupArtPiece());
    }

    IEnumerator SetupArtPiece()
    {
        yield return ArtManagerInterface.RequestRandomArt(ArtPiece);
        UpdateUI();
    }

    void UpdateUI()
    {
        // Setup and scale artwork
        artwork.enabled = false;
        var rect = new Rect(0, 0, ArtPiece.Art.width, ArtPiece.Art.height);
        artwork.sprite = Sprite.Create(ArtPiece.Art, rect, artwork.transform.position);
        artwork.type = Image.Type.Simple;
        artwork.preserveAspect = true;
        Debug.Log($"Art Size: {ArtPiece.Art.width} by {ArtPiece.Art.height}");
        aspectFitter.aspectRatio = (float)ArtPiece.Art.width / ArtPiece.Art.height;
        artwork.enabled = true;

        title.text = ArtPiece.Title;
        artist.text = ArtPiece.Artist;
        description.text = ArtPiece.Description;
    }
}
