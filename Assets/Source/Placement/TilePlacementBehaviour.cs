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
    public class TilePlacementBehaviour : PlacementBehaviourBase<TilePlacement>
    {
        // TODO: Create a generic DragListener class and refactor this to use it.
        public override bool Busy => _current != null;
        private TilePlacement _current;

        private InputAction[] _onClick;

        private void Start()
        {
            InputMaster master = new InputMaster();

            _onClick = new InputAction[2];
            _onClick[0] = master.Dragging.PrimaryDrag;
            _onClick[1] = master.Dragging.SecondaryDrag;

            master.General.Enable();
            master.Enable();
        }

        public override void Cancel()
        {
            if (_current != null)
            {
                _current.Finish();
                _current = null;
            }
        }

        public override void TakePlacement(TilePlacement placement)
        {
            _current = placement;
        }

        public void Update()
        {
            if (_current != null)
            {
                Vector2 position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _current.ToPosition(position);

                if (!UIUtils.IsOverUI(Mouse.current.position.ReadValue()))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (_onClick[i].phase == InputActionPhase.Performed)
                        {
                            _current.StartDrag(i, position);
                        }
                        if (_onClick[i].ReadValue<float>() == 1)
                        {
                            _current.Drag(i, position);
                        }
                        if (_onClick[i].phase == InputActionPhase.Canceled)
                        {
                            _current.EndDrag(i, position);
                        }
                    }
                }
            }
        }
    }
}
