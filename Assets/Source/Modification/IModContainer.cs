using Lomztein.BFA2.Modification.Modifiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification
{
    public interface IModContainer
    {
        void AddMod(IMod mod);

        void RemoveMod(IMod mod);
    }
}
