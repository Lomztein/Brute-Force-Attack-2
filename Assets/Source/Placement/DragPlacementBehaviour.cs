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
    public class DragPlacementBehaviour : PlacementBehaviourBase<TilePlacement>
    {
        public override bool Busy => _current != null;
        private TilePlacement _current;

        private void Start()
        {
            DragListener.Create(StartDrag, UpdateDrag, EndDrag);
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

        private void StartDrag (int mouse, DragListener.Drag drag)
        {
            if (_current != null && !UIUtils.IsOverUI(drag.ScreenStart))
            {
                _current.StartDrag(mouse, Camera.main.ScreenToWorldPoint(drag.ScreenStart));
            }
        }

        private void UpdateDrag (int mouse, DragListener.Drag drag)
        {
            if (_current != null && !UIUtils.IsOverUI(drag.ScreenPosition))
            {
                _current.Drag(mouse, Camera.main.ScreenToWorldPoint(drag.ScreenPosition));
            }
        }

        private void EndDrag (int mouse, DragListener.Drag drag)
        {
            if (_current != null && !UIUtils.IsOverUI(drag.ScreenPosition))
            {
                _current.EndDrag(mouse, Camera.main.ScreenToWorldPoint(drag.ScreenPosition));
            }
        }

        private void Update()
        {
            if (_current != null)
            {
                _current.ToPosition(Input.WorldMousePosition);
            }
        }
    }
}
