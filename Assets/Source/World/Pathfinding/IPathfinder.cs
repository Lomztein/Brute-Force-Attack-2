using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.World.Pathfinding
{
    public interface IPathfinder
    {
        Vector3[] Search(Vector3 start, Vector3[] end);
    }
}
