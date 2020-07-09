using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Content.References.ReferenceComponents
{
    public abstract class ReferenceComponentBase : MonoBehaviour
    {
        [SerializeField]
        [HideInInspector]
        private bool _applied = false;

        public void OnAssembled()
        {
            TryApply();
        }

        private void Start()
        {
            TryApply();
        }

        private void TryApply ()
        {
            if (!_applied)
            {
                _applied = true;
                Apply();
            }
        }

        protected abstract void Apply();
    }
}
