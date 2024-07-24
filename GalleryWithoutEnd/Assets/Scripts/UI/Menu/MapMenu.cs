using System.Collections;
using System.Collections.Generic;
using GameManagers;
using UnityEngine;

namespace GalleryWithoutEnd.UI.Menus
{
    public class MapMenu : Menu
    {
        [SerializeField] private Camera _mapCamera;

        private void Start()
        {
            _mapCamera = Instantiate(_mapCamera);

            var follower = PlayerManager.Instance.PlayerCamera.GetComponent<FollowTransform>();
            _mapCamera.GetComponent<FollowTransform>().target = follower.target;
        }

        private void OnDestroy()
        {
            Destroy(_mapCamera);
        }
    }
}
