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
            position = snapToGrid ? GridDimensions.SnapToGrid(position, Size.Small) : position;
            _handle.transform.position = position;
        }
        
        public void Rotate (float amount, bool snap)
        {
            _handle.transform.Rotate(new Vector3(0f, 0f, amount));
        }

        public void Delete ()
        {
            _handle.Destroy();
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
