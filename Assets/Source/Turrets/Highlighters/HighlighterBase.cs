using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.Highlighters
{
    public abstract class HighlighterBase<T> : MonoBehaviour, IHighlighter where T : class
    {
        private Transform _parent;

        public bool CanHighlight(Type componentType)
        {
            return typeof (T).IsAssignableFrom (componentType);
        }

        public abstract void EndHighlight();
        public abstract void Highlight(T component);

        public void Highlight(Component component)
        {
            _parent = component.transform;
            Highlight(component as T);
        }

        private void Update()
        {
            transform.position = _parent.position;
        }
    }
}
