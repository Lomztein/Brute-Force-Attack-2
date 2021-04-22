using System;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem.Objects.Assemblers.ComponentConverters
{
    public interface IComponentConverter
    {
        bool CanConvert(Type type);

        Component ConvertComponent(Component component, GameObject target);
    }
}