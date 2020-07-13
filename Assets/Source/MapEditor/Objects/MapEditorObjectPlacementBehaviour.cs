using Lomztein.BFA2.Placement;
using System;
using System.Collections;
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
            bool placedThisFrame = false;

            if (_current != null)
            {
                _current.ToPosition(mousePos, Input.GetKey(KeyCode.LeftControl));
                _current.Rotate(Input.GetAxis("Mouse ScrollWheel") * 30f, false);

                if (Input.GetMouseButtonDown(0))
                {
                    _current.Place();
                    placedThisFrame = true;
                }
                if (Input.GetMouseButtonDown(1))
                {
                    _current.Delete();
                }
            }

            Collider2D collider = Physics2D.OverlapPointAll(mousePos).Where(x => x.GetComponent<MapObjectHandle>() != null).FirstOrDefault();
            if (collider && Input.GetMouseButtonDown(0) && !placedThisFrame)
            {
                MapObjectPlacement placement = new MapObjectPlacement();
                placement.Pickup(collider.GetComponent<MapObjectHandle>());
                TakePlacement(placement);
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
