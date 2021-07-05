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

        public void Select (MapObjectHandle handle)
        {
            _handle = handle;
            _handle.OnSelected();
        }

        public void Delete ()
        {
            if (_handle)
            {
                _handle.Delete();
            }
            Finish();
        }

        public void Deselect ()
        {
            Finish();
        }

        public bool Finish()
        {
            if (_handle)
            {
                _handle.OnDeselected();
                _handle = null;
            }

            OnFinished?.Invoke();
            return true;
        }

        public void Init()
        {
        }
    }
}
