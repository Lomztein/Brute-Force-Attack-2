using UnityEngine;
using System.Collections;
using Lomztein.BFA2.Weaponary.Projectiles;
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Collider.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (Input.GetMouseButtonDown(MouseButton))
                {
                    OnClick.Invoke();
                }
            }
        }
    }
}