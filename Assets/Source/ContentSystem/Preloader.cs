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
        public static Preloader Instance { get; private set; }

        public bool PreloadOnContentReload;
        public bool PreloadOnAwake;

        private static bool _hasPreloaded;

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
            Instance = this;
            ContentManager.OnPostContentReload += ContentManager_OnPostContentReload;
            if (PreloadOnAwake)
            {
                BeginPreload();
            }
        }

        private void OnDestroy()
        {
            ContentManager.OnPostContentReload -= ContentManager_OnPostContentReload;
        }

        private void ContentManager_OnPostContentReload(IEnumerable<IContentPack> obj)
        {
            _hasPreloaded = false;
            if (PreloadOnContentReload)
            {
                BeginPreload();
            }
        }

        private static IEnumerable<string> GetPreloadPaths()
            => Content.GetAll<string>("*/Preloads/*.txt").SelectMany(x => Regex.Split(x, "\r\n|\r|\n")).Distinct();

        public void BeginPreload ()
        {
            Done = false;
            if (!_hasPreloaded)
            {
                StartCoroutine(Preload(GetPreloadPaths()));
                _hasPreloaded = true;
            }
            else
            {
                Done = true;
            }
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
                var currentEnumerator = Content.GetAll(path, type).GetEnumerator();

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
