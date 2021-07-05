using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Lomztein.BFA2.Utilities
{
    public class WorldColliderButton : MonoBehaviour
    {
        public Collider Collider;
        public UnityEvent OnClick;

        private void Start()
        {
            Input.PrimaryClickStarted += PrimaryClick;
        }

        private void OnDestroy()
        {
            Input.PrimaryClickStarted -= PrimaryClick;
        }

        private void PrimaryClick(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.ScreenMousePosition);
            if (Collider.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (Input.PrimaryPhase == UnityEngine.InputSystem.InputActionPhase.Started)
                {
                    OnClick.Invoke();
                }
            }
        }
    }
}