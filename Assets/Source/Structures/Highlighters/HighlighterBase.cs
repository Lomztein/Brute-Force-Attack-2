using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Highlighters
{
    public abstract class HighlighterBase<T> : MonoBehaviour, IHighlighter where T : class
    {
        private Transform _parent;

        public bool CanHighlight(Type componentType)
        {
            return typeof (T).IsAssignableFrom (componentType);
        }

        public virtual void EndHighlight()
        {
            Destroy(gameObject);
        }

        public abstract void Highlight(T component);

        public void Highlight(Component component)
        {
            _parent = component.transform;

            transform.position = _parent.position;
            transform.rotation = _parent.rotation;

            Highlight(component as T);
            Tick(Time.fixedDeltaTime);
        }

        public abstract void Tint(Color color);

        public virtual void Tick(float deltaTime) { }

        private void Update()
        {
            transform.position = _parent.position;
            transform.rotation = _parent.rotation;
        }

        private void FixedUpdate()
        {
            Tick(Time.fixedDeltaTime);
        }
    }
}
