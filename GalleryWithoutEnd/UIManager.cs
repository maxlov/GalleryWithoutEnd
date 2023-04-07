using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InputReader inputReader = default;
    [SerializeField] private GameObject menu;

    #region Menu Toggle

    private void OnEnable()
    {
        inputReader.pauseEvent += ToggleMenu;
    }

    private void OnDisable()
    {
        inputReader.pauseEvent -= ToggleMenu;
    }

    /// <summary>
    /// Opens or closes menu based on state of menu GameObject
    /// </summary>
    private void ToggleMenu()
    {
        if (menu.activeSelf)
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
        menu.SetActive(true);
    }

    /// <summary>
    /// Deactivates the menu GameObject, locks and hides cursor
    /// </summary>
    public void CloseMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menu.SetActive(false);
    }

    #endregion
}
