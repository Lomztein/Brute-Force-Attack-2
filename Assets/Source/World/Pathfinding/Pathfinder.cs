using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.World.Pathfinding
{
    public class Pathfinder : MonoBehaviour
    {
        public static Pathfinder Instance;
        private IPathfinder _pathfinder;

        public void Awake()
        {
            Instance = this;
            _pathfinder = GetComponent<IPathfinder>();
        }
        public static Vector3[] Search(Vector3 start, params Vector3[] end) => Instance._pathfinder.Search(start, end);
    }
}