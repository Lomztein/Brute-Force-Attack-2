using UnityEngine;
using System.Collections;

namespace Lomztein.BFA2.MapEditor.Objects.ComponentHandlers
{
    public interface IComponentHandle
    {
        bool CanHandle(Component component);

        void Assign(Component component);
    }
}