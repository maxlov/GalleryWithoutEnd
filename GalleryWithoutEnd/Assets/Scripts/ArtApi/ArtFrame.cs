using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ArtFrame : MonoBehaviour
{
    [SerializeField] private AspectRatioFitter aspectFitter;
    [SerializeField] private RectTransform frame;

    [SerializeField] private RawImage artwork;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI artist;
    [SerializeField] private TextMeshProUGUI description;

    private ArtSO ArtPiece;

    private void Start()
    {
        ArtPiece = ScriptableObject.CreateInstance<ArtSO>();
        ArtPiece.ReceivedData.AddListener(UpdateUI);
        GetArtFromManager();
    }

    /// <summary>
    /// Calls art manager to set up ArtSO
    /// </summary>
    public void GetArtFromManager()
    {
        BaseApiManager.Instance.QueueArtRequest(ArtPiece);
    }


    /// <summary>
    /// Sets up sprite with art, scales UI, and applies ArtSO text to text UI
    /// </summary>
    void UpdateUI()
    {
        artwork.enabled = false;

        var rect = new Rect(0, 0, ArtPiece.Art.width, ArtPiece.Art.height);

        artwork.texture = ArtPiece.Art;
        artwork.SetNativeSize();

        SetupAspectRatio();

        artwork.enabled = true;

        title.text = ArtPiece.Title;
        artist.text = ArtPiece.Artist;
        description.text = ArtPiece.Description;
    }

    /// <summary>
    /// Sets up aspect ratio based on image size
    /// </summary>
    void SetupAspectRatio()
    {
        float aspectRatio = (float)ArtPiece.Art.width / ArtPiece.Art.height;
        aspectFitter.aspectRatio = aspectRatio;

        aspectRatio = Mathf.Round(aspectRatio * 100.0f) * 0.01f;

        frame.sizeDelta = aspectRatio switch
        {
            > 2 => new Vector2(550, 285),
            > 1 => new Vector2(550, 420),
            < 1 => new Vector2(350, 420),
            _ => new Vector2(420, 420)
        };
    }
}
