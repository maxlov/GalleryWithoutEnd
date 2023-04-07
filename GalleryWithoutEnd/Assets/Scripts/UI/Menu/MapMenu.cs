using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GalleryWithoutEnd.UI.Menus
{
    public class MapMenu : Menu
    {
        [SerializeField] private Camera _mapCamera;

        private void Start()
        {
            _mapCamera = Instantiate(_mapCamera);
            
            var follower = _mapCamera.GetComponent<FollowTransform>();

            if (follower)
                follower.target = PlayerManager.Instance.Player.transform;
        }

        private void OnDestroy()
        {
            Destroy(_mapCamera);
        }
    }
}
