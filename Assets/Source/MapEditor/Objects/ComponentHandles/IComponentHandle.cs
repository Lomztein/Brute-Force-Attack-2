using UnityEngine;
using System.Collections;
using UnityEditor.Build;

namespace Lomztein.BFA2.MapEditor.Objects.ComponentHandlers
{
    public interface IComponentHandle
    {
        bool CanHandle(Component component);

        void Assign(Component component);
    }
}