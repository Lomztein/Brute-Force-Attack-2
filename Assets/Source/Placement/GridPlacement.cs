using Lomztein.BFA2.Grid;
using Lomztein.BFA2.Turrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Placement
{
    public class GridPlacement : IPlacement
    {
        private GameObject _obj;
        private IGridPlaceable _placeable;
        private LayerMask _blockingLayer;
        
        public bool Pickup(GameObject obj)
        {
            _obj = obj;
            _placeable = obj.GetComponent<IGridPlaceable>();
            _placeable.Pickup();
            return _placeable != null;
        }

        public bool Place()
        {
            if (CanPlace (_obj.transform.position, _obj.transform.rotation))
            {
                _placeable.Place();
                return true;
            }
            return false;
        }

        public bool ToPosition(Vector2 position, Quaternion rotation)
        {
            position = Snap(position);
            _obj.transform.position = position;
            _obj.transform.rotation = rotation;
            return CanPlace(position, rotation);
        }

        private bool CanPlace (Vector2 position, Quaternion rotation)
        {
            float radius = GridDimensions.SizeOf (_placeable.Size) / 2f;
            return !Physics2D.OverlapCircle(position, radius);
        }

        private Vector2 Snap (Vector2 position)
        {
            float size = GridDimensions.CELL_SIZE;

            float x = Mathf.Round(position.x / size) * size;
            float y = Mathf.Round(position.y / size) * size;
            bool even = GridDimensions.IsEven(_placeable.Size);

            return new Vector2(x, y) + (even ? new Vector2 (size / 2f, size / 2f) : Vector2.zero);
        }

        public bool ToTransform(Transform transform)
        {
            return false;
        }
    }
}
