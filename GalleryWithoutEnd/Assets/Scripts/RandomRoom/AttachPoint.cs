using System.Collections;
using UnityEngine;

namespace RandomRoom
{
    public class AttachPoint : MonoBehaviour
    {
        [HideInInspector] public RoomManager RoomManager;

        private void OnMouseDown()
        {
            RoomManager.CreateRoom(transform);
        }
    }
}