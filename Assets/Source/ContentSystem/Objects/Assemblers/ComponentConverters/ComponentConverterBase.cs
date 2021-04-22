using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Objects.Assemblers.ComponentConverters
{
    public abstract class ComponentConverterBase<T> : IComponentConverter where T : Component
    {
        public bool CanConvert(Type type) => type == typeof(T);

        public Component ConvertComponent(Component component, GameObject target) => ConvertComponent((T)component, target);

        public abstract Component ConvertComponent(T component, GameObject target);
    }
}
