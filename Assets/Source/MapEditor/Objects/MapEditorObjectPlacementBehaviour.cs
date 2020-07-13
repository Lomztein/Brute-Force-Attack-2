using Lomztein.BFA2.Placement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.MapEditor.Objects
{
    public class MapEditorObjectPlacementBehaviour : PlacementBehaviourBase<MapObjectPlacement>
    {
        public override bool Busy => _current != null;
        private MapObjectPlacement _current;

        public override void Cancel()
        {
            if (_current != null)
            {
                _current.Finish();
                _current = null;
            }
        }

        private void Update()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (_current != null)
            {
                _current.ToPosition(mousePos, Input.GetKey(KeyCode.LeftControl));
                _current.Rotate(Input.GetAxis("Mouse ScrollWheel") * 15f, false);

                if (Input.GetMouseButtonDown(0))
                {
                    _current.Place();
                }
                if (Input.GetMouseButtonDown(1))
                {
                    _current.Delete();
                }
            }
        }

        public override void TakePlacement(MapObjectPlacement placement)
        {
            _current = placement;
            _current.OnFinished += () =>
            {
                _current = null;
            };
        }
    }
}
