using Lomztein.BFA2.Modification.Modifiers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification
{
    public interface IModContainer
    {
        Mod[] Mods { get; }

        void AddMod(Mod mod);

        void RemoveMod(string identifier);
    }
}
