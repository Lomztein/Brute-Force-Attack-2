using Lomztein.BFA2.Player.Messages;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Placement
{
    public class SimplePlacementBehaviour : PlacementBehaviourBase<ISimplePlacement>
    {
        private ISimplePlacement _currentPlaceable;

        public static SimplePlacementBehaviour Instance { get; private set; }
        public override bool Busy => _currentPlaceable != null;

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
                if (Input.GetMouseButtonDown(0) && !UIUtils.IsOverUI(Input.mousePosition))
                {
                    if (!PlaceCurrent())
                    {
                        Message.Send("Cannot place here.", Message.Type.Minor);
                    }
                }
                if (Input.GetMouseButtonDown(1))
                {
                    Cancel();
                }
            }
        }

        public override void Cancel()
        {
            if (_currentPlaceable != null && _currentPlaceable.Finish())
            {
                _currentPlaceable = null;
            }
        }

        public override void TakePlacement(ISimplePlacement placeable)
        {
            _currentPlaceable = placeable;
        }

        public bool PlaceCurrent()
        {
            if (_currentPlaceable.Place())
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    Cancel();
                }
                return true;
            }
            return false;
        }

        public string CurrentToString() => _currentPlaceable?.ToString() ?? "No current placement.";
    }
}
