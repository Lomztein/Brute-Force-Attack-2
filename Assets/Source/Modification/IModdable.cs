using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification
{
    public interface IModdable
    {
        IModContainer Mods { get; }
    }
}