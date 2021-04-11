using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lomztein.BFA2
{
    public class MousePosition : MonoBehaviour
    {
        public static Vector3 ScreenPosition { get; private set; }
        public static Vector3 WorldPosition { get; private set; }

        private void Update()
        {
            ScreenPosition = Mouse.current.position.ReadValue();
            WorldPosition = Camera.main.ScreenToWorldPoint(ScreenPosition);
        }
    }
}
