using System.Collections.Generic;
using UnityEngine;

namespace RandomRoom
{
    [CreateAssetMenu(fileName = "RoomManager", menuName = "RoomGeneration/RoomManager")]
    public class RoomManager : ScriptableObject
    {
        [SerializeField] private List<GameObject> RoomPrefabs;

        public void CreateRoom(Transform point)
        {
            GameObject room = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Count)], Vector3.zero, Quaternion.identity);
            RoomNode roomNode = room.GetComponent<RoomNode>();
            Transform newPoint = roomNode.GetRandomPoint().transform;

            var newPointRotation = Quaternion.FromToRotation(newPoint.forward, -point.forward);
            room.transform.rotation *= newPointRotation;

            if (room.transform.rotation.eulerAngles == new Vector3(0, 180, 180))
                room.transform.rotation = Quaternion.Euler(0, 180, 0);

            Vector3 offset = room.transform.position - newPoint.position;
            room.transform.position = point.position + offset;

            point.transform.gameObject.SetActive(false);
            newPoint.transform.gameObject.SetActive(false);

            roomNode.RoomContent.SetActive(true);
        }
    }
}
