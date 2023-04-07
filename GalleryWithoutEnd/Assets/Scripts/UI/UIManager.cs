using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GalleryWithoutEnd.UI.Menus;

namespace GalleryWithoutEnd.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader = default;
        [SerializeField] private GameObject _UI;

        [SerializeField] private GameObject _menuPanel;
        [SerializeField] private Menu _activeMenu;

        #region Menu Toggle

        private void OnEnable()
        {
            _inputReader.pauseEvent += ToggleMenu;
        }

        private void OnDisable()
        {
            _inputReader.pauseEvent -= ToggleMenu;
        }

        /// <summary>
        /// Opens or closes menu based on state of menu GameObject
        /// </summary>
        private void ToggleMenu()
        {
            if (_UI.activeSelf)
                CloseMenu();
            else
                OpenMenu();
        }

        /// <summary>
        /// Activates the menu GameObject, unlocks and shows cursor
        /// </summary>
        private void OpenMenu()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _UI.SetActive(true);
        }

        /// <summary>
        /// Deactivates the menu GameObject, locks and hides cursor
        /// </summary>
        public void CloseMenu()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _UI.SetActive(false);
        }

        #endregion

        public void OpenMenu(Menu nextMenu)
        {
            _activeMenu.gameObject.SetActive(false);
            Destroy(_activeMenu);
            _activeMenu = Instantiate(nextMenu.gameObject, _menuPanel.transform).GetComponent<Menu>();
        }



    }
}