using Lomztein.BFA2.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Placement
{
    public class TilePlacementBehaviour : PlacementBehaviourBase<TilePlacement>
    {
        public override bool Busy => _current != null;
        private TilePlacement _current;

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
                Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _current.ToPosition(position);

                if (!UIUtils.IsOverUI(Input.mousePosition))
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (Input.GetMouseButtonDown(i))
                        {
                            _current.StartDrag(i, position);
                        }
                        if (Input.GetMouseButton(i))
                        {
                            _current.Drag(i, position);
                        }
                        if (Input.GetMouseButtonUp(i))
                        {
                            _current.EndDrag(i, position);
                        }
                    }
                }
            }
        }
    }
}
