using System.Collections;
using System.Collections.Generic;
using RandomRoom;
using UnityEngine;

public class RoomCollisionPoint : MonoBehaviour
{
    [HideInInspector] public RoomManager RoomManager;
    [HideInInspector] public RoomNode Room;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        if (Room.randomPoints.Count == 0)
            return;

        Room.ExtendRoom();
    }
}
