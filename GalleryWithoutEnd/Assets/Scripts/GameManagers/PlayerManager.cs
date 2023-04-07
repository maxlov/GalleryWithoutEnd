using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] public GameObject Player;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
    }
}
