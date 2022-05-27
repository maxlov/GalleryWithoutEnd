using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IApiManager
{
    // Interface so ArtFrame will always be happy with whichever API is being used.
    // Might be better to use a baseclass instead

    public void QueueArtRequest(ArtSO artPiece);
}
