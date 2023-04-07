using System;
using System.Collections;
using UnityEngine;

public abstract class BaseApiManager : MonoBehaviour
{
    public static BaseApiManager Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public abstract void QueueArtRequest(ArtSO artPiece);
}
