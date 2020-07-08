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

        private void Update()
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3[] path = Search(Vector3.zero, mouse);

            if (path != null)
            {
                for (int i = 0; i < path.Length - 1; i++)
                {
                    Debug.DrawLine(path[i], path[i + 1]);
                }
            }
        }

        public static Vector3[] Search(Vector3 start, params Vector3[] end) => Instance._pathfinder.Search(start, end);
    }
}