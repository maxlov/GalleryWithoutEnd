using System.Collections;
using UnityEngine;

namespace RandomRoom
{
    public class AttachPoint : MonoBehaviour
    {
        [SerializeField] RoomManager roomManager;

        private void OnMouseDown()
        {
            roomManager.CreateRoom(transform);
        }
    }
}