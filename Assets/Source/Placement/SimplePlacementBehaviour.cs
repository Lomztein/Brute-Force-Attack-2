using Lomztein.BFA2.UI.Messages;
using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Lomztein.BFA2.Placement
{
    public class SimplePlacementBehaviour : PlacementBehaviourBase<ISimplePlacement>
    {
        private ISimplePlacement _currentPlaceable;

        public static SimplePlacementBehaviour Instance { get; private set; }
        public override bool Busy => _currentPlaceable != null;

        private InputAction _quickPlace;

        private void Awake()
        {
            Instance = this;

            InputMaster master = new InputMaster();

            master.General.PrimaryClick.performed += OnPrimaryClick;
            master.General.SecondaryClick.performed += OnSecondaryClick;
            _quickPlace = master.Placement.QuickPlace;
            master.Placement.Enable();
            master.General.Enable();
            master.Enable();
        }

        private void OnSecondaryClick(InputAction.CallbackContext obj)
        {
            Cancel();
        }

        private void OnPrimaryClick(InputAction.CallbackContext obj)
        {
            if (!UIUtils.IsOverUI(Mouse.current.position.ReadValue()))
            {
                if (!PlaceCurrent())
                {
                    Message.Send("Cannot place here.", Message.Type.Minor);
                }
            }
        }

        private void Update()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            if (_currentPlaceable != null)
            {
                _currentPlaceable.ToPosition(mousePos, Quaternion.identity);
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
            if (_currentPlaceable != null)
            {
                if (_currentPlaceable.Place())
                {
                    if (_quickPlace.phase == InputActionPhase.Waiting)
                    {
                        Cancel();
                    }
                    return true;
                }
            }
            return false;
        }

        public string CurrentToString() => _currentPlaceable?.ToString() ?? "No current placement.";
    }
}
