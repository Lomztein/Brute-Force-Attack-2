using UnityEngine;
using System.Collections;

namespace Lomztein.BFA2.MapEditor.Objects.ComponentHandlers
{
    public abstract class ComponentHandleBase<T> : MonoBehaviour, IComponentHandle where T : Component
    {
        public void Assign(Component component) => Assign(component as T);

        public abstract void Assign(T component);

        public bool CanHandle(Component component) => typeof(T).IsAssignableFrom(component.GetType());
    }
}