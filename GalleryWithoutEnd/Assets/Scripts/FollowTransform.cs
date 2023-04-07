using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform target;

    [SerializeField] private bool lockHeight;

    private void Update()
    {
        if (!target)
            return;

        var targetXYZ = target.position;

        if (lockHeight)
            targetXYZ.y = transform.position.y;

        transform.position = targetXYZ;
    }
}
