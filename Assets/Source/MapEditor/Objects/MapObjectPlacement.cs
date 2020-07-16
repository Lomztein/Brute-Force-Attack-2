using Lomztein.BFA2.Grid;
using Lomztein.BFA2.Placement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.MapEditor.Objects
{
    public class MapObjectPlacement : IPlacement
    {
        public event Action OnFinished;
        private MapObjectHandle _handle;

        public void Pickup (MapObjectHandle handle)
        {
            _handle = handle;
        }

        public void ToPosition(Vector2 position, bool snapToGrid)
        {
            position = snapToGrid ? GridDimensions.SnapToGrid(position, ComputeSize(_handle.Bounds.size.x), ComputeSize(_handle.Bounds.size.y)) : position;
            _handle.transform.position = position;
        }

        private Size ComputeSize(float size)
            => Mathf.RoundToInt(size) % 2 == 0 ? Size.Medium : Size.Small;
        
        public void Rotate (float amount, bool snap)
        {
            _handle.transform.Rotate(new Vector3(0f, 0f, amount));
        }

        public void Delete ()
        {
            _handle.Delete();
            Finish();
        }

        public void Place ()
        {
            _handle = null;
            Finish();
        }

        public bool Finish()
        {
            OnFinished?.Invoke();
            return true;
        }
    }
}
