using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Targeting
{
    public interface ITargetFinder
    {
        Transform FindTarget(Collider2D[] options);
    }
}