using UnityEngine;

namespace GameManagers
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        [field: SerializeField] public GameObject Player { get; set; }
        [field: SerializeField] public Camera PlayerCamera { get; set; }

        private void Awake()
        {
            if (!Instance)
                Instance = this;
        }
    }
}
