using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtFrame2D : MonoBehaviour
{
    [SerializeField] private GameObject ArtManager;
    private IApiManager ArtManagerInterface;

    [SerializeField] private AspectRatioFitter aspectFitter;
    [SerializeField] private RectTransform frame;

    [SerializeField] private Image artwork;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI artist;
    [SerializeField] private TextMeshProUGUI description;

    private ArtSO ArtPiece;

    private void Start()
    {
        // Create artpiece instance to use for frame
        ArtPiece = ScriptableObject.CreateInstance<ArtSO>();

        // Get IApiManager, used because unity doesn't like serialized interfaces
        if (!ArtManager.TryGetComponent<IApiManager>(out ArtManagerInterface))
            Debug.LogError("No IApiManager component found on ArtManager");
    }

    /// <summary>
    /// Starts coroutine to set up ArtSO using ArtManager
    /// </summary>
    public void GetArtFromManager()
    {
        // Coroutine to wait until artSO is properly setup before updating UI
        StartCoroutine(SetupArtPiece());
    }

    /// <summary>
    /// Calls the art manager to update the ArtSO and then updates UI
    /// </summary>
    IEnumerator SetupArtPiece()
    {
        yield return ArtManagerInterface.RequestRandomArt(ArtPiece);
        UpdateUI();
    }

    /// <summary>
    /// Sets up sprite with art, scales UI, and applies ArtSO text to text UI
    /// </summary>
    void UpdateUI()
    {
        // Setup and scale artwork

        artwork.enabled = false;

        // Creates new rect for artwork, then creates sprite with it
        var rect = new Rect(0, 0, ArtPiece.Art.width, ArtPiece.Art.height);
        artwork.sprite = Sprite.Create(ArtPiece.Art, rect, artwork.transform.position);

        artwork.type = Image.Type.Simple;
        artwork.preserveAspect = true;

        // Sets up aspect ratio so the fitter functions corecctly
        float aspectRatio = (float)ArtPiece.Art.width / ArtPiece.Art.height;
        aspectFitter.aspectRatio = aspectRatio;

        // Round aspect ratio to 2 decimal places to more easily
        // make frame for square aspect ratio 
        aspectRatio = Mathf.Round(aspectRatio * 100.0f) * 0.01f;

        // Select frame based on aspect ratio, for landscape, portrait, and square respectively
        if (aspectRatio > 1)
            frame.sizeDelta = new Vector2(550, 420);
        else if (aspectRatio < 1)
            frame.sizeDelta = new Vector2(350, 420);
        else
            frame.sizeDelta = new Vector2(420, 420);

        artwork.enabled = true;

        title.text = ArtPiece.Title;
        artist.text = ArtPiece.Artist;
        description.text = ArtPiece.Description;
    }
}
