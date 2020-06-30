using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Placement
{
    public class PlacementController : MonoBehaviour
    {
        private IPlacement _currentPlaceable;
        public static PlacementController Instance { get; private set; }
        public bool Busy => _currentPlaceable != null;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (_currentPlaceable != null)
            {
                _currentPlaceable.ToPosition(mousePos, Quaternion.identity);
                if (Input.GetMouseButtonDown(0))
                {
                    PlaceCurrent();
                }
                if (Input.GetMouseButtonDown(1))
                {
                    FinishCurrent();
                }
            }
        }

        private void FinishCurrent()
        {
            if (_currentPlaceable.Finish())
            {
                _currentPlaceable = null;
            }
        }

        public bool PickUp(IPlacement placeable, GameObject obj)
        {
            if (_currentPlaceable != null)
            {
                FinishCurrent();
            }
            if (placeable.Pickup(obj))
            {
                _currentPlaceable = placeable;
                return true;
            }
            return false;
        }

        public bool PlaceCurrent()
        {
            if (_currentPlaceable.Place())
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    FinishCurrent();
                }
                return true;
            }
            return false;
        }

        public string CurrentToString() => _currentPlaceable?.ToString() ?? "No current placement.";
    }
}
