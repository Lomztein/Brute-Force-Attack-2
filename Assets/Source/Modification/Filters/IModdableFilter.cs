using Lomztein.BFA2.Structures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Filters
{
    public interface IModdableFilter
    {
        bool Check(IModdable moddable);
    }
}