using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IApiManager
{
    public IEnumerator RequestRandomArt(ArtSO artPiece);
}
