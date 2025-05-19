using System;
using UnityEngine;

namespace Game.UI
{
    public class CursorBehaviour
    {
        public void Hide()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Show()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}