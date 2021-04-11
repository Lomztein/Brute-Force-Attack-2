using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Lomztein.BFA2.Utilities
{
    public class WorldColliderButton : MonoBehaviour
    {
        public int MouseButton;
        public Collider Collider;

        public UnityEvent OnClick;

        public void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.ScreenMousePosition);
            if (Collider.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (MouseButton == 0 ? Input.PrimaryDown : Input.SecondaryDown)
                {
                    OnClick.Invoke();
                }
            }
        }
    }
}