using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification
{
    public enum ModdableAttribute
    {
        Ranged,
        Rotator,
        Projectile,
        Hitscan,
        Homing,
        Assembly,
        Weapon,
        Base,
    }

    public interface IModdable
    {
        IModContainer Mods { get; }

        ModdableAttribute[] Attributes { get; }
    }
}