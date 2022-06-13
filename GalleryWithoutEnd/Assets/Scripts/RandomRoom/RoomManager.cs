using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RandomRoom
{
    [CreateAssetMenu(fileName = "RoomManager", menuName = "RoomGeneration/RoomManager")]
    public class RoomManager : ScriptableObject
    {
        [SerializeField] private List<GameObject> RoomPrefabs;
        [SerializeField] private GameObject BlockedPrefab;

        private void OnEnable()
        {
            if (RoomPrefabs == null || RoomPrefabs.Count == 0)
                Debug.LogError("No rooms set in RoomManager " + this.name);
            if (BlockedPrefab == null)
                Debug.LogError("No prefab set for BlockedPrefab in RoomManager " + this.name);
        }

        public void CreateRoom(Transform point)
        {
            var randomPrefabList = RoomPrefabs.ToList();
            ShuffleList(randomPrefabList);

            foreach (var roomObject in randomPrefabList)
            {
                GameObject room = Instantiate(roomObject, Vector3.zero, Quaternion.identity);
                var roomNode = room.GetComponent<RoomNode>();

                if (roomNode.SetupRoom(point))
                {
                    roomNode.RoomContent.SetActive(true);
                    return;
                }

                Destroy(room);
            }

            var blockNode = Instantiate(BlockedPrefab, Vector3.zero, Quaternion.identity).GetComponent<RoomNode>();
            blockNode.SetupRoom(point);
        }

        /// <summary>
        /// Randomizes list, so it can be looped over randomly without repeat.
        /// </summary>
        /// <param name="objectList">List to randomize</param>
        private static void ShuffleList(List<GameObject> objectList)
        {
            for (var i = 0; i < objectList.Count; i++)
            {
                var random = Random.Range(i, objectList.Count);
                (objectList[random], objectList[i]) = (objectList[i], objectList[random]);
            }
        }
    }
}
