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

        public bool Done { get; private set; }

        public int CurrentStep { get; private set; }
        public int TotalSteps { get; private set; }
        public int StepLoadTimeMillis { get; private set; }
        public string StepName { get; private set; }

        public int CurrentPiece { get; private set; }
        public int TotalPieces { get; private set; }
        public int PieceLoadTimeMillis { get; private set; }
        public string PieceName { get; private set; }

        private void Awake()
        {
            if (BeginOnAwake) BeginPreload();
        }

        private static IEnumerable<string> GetPreloadPaths()
            => Content.GetAll<string>("*/Preloads/*.txt").SelectMany(x => Regex.Split(x, "\r\n|\r|\n")).Distinct();

        public void BeginPreload ()
        {
            StartCoroutine(Preload(GetPreloadPaths()));
        }

        private IEnumerator Preload(IEnumerable<string> preloads)
        {
            TotalSteps = preloads.Count();
            CurrentStep = 0;
            foreach (var preload in preloads)
            {
                StepName = preload;
                var start = DateTime.Now;
                string[] split = preload.Split(' ');
                string path = split[0];
                Type type = Type.GetType(split[1]);

                var pieces = Content.QueryContentIndex(path).ToArray();
                TotalPieces = pieces.Length;
                CurrentPiece = 0;
                var currentEnumerator = Content.LoadAll(path, type).GetEnumerator();

                while (currentEnumerator.MoveNext())
                {
                    PieceName = pieces[CurrentPiece];
                    yield return currentEnumerator.Current;
                    CurrentPiece++;
                }

                yield return Content.LoadAll(path, type).GetEnumerator();
                Debug.Log($"Loaded '{preload}' in {(DateTime.Now - start).Milliseconds} milliseconds.");
                CurrentStep++;
            }
            Done = true;
        }
    }
}
