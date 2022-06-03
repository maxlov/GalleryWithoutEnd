using System.Collections.Generic;
using UnityEngine;

namespace RandomRoom
{
    public class RoomNode : MonoBehaviour
    {
        public List<AttachPoint> AttachPoints;
        public GameObject RoomContent;

        public AttachPoint GetRandomPoint()
        {
            return AttachPoints[Random.Range(0, AttachPoints.Count)];
        }
    }
}
