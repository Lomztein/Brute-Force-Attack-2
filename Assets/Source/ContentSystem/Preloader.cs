using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Lomztein.BFA2.ContentSystem
{
    public class Preloader : MonoBehaviour
    {
        public bool BeginOnAwake;

        public float PreloadProgress { get; private set; }
        public string CurrentPreload { get; private set; }
        public bool Done { get; private set; }

        private void Awake()
        {
            if (BeginOnAwake) BeginPreload();
        }

        private static IEnumerable<string> GetPreloadPaths()
            => Content.GetAll<string>("*/Preloads").SelectMany(x => Regex.Split(x, "\r\n|\r|\n")).Distinct();

        public void BeginPreload ()
        {
            StartCoroutine(Preload(GetPreloadPaths()));
        }

        private IEnumerator Preload(IEnumerable<string> preloads)
        {
            int total = preloads.Count();
            int current = 0;
            foreach (var preload in preloads)
            {
                CurrentPreload = preload;
                string[] split = preload.Split(' ');
                string path = split[0];
                Type type = Type.GetType(split[1]);
                Content.GetAll(path, type);
                current++;
                PreloadProgress = (float)current / total;
                yield return null;
            }
            Done = true;
        }
    }
}
