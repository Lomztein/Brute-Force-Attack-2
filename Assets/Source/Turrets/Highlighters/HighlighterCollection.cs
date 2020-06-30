using Lomztein.BFA2.Content.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.Highlighters
{
    public class HighlighterCollection
    {
        private static IContentCachedPrefab[] _highlighterPrefabs;
        private const string PREFAB_PATH = "*/Highlighters";

        private IHighlighter[] _highlighters;
        private Component[] _components;

        public HighlighterCollection (IHighlighter[] highlighters, Component[] components)
        {
            _highlighters = highlighters;
            _components = components;
        }

        public void Highlight ()
        {
            for (int i = 0; i < _highlighters.Length; i++)
            {
                _highlighters[i].Highlight(_components[i]);
            }
        }

        public void EndHighlight()
        {
            for (int i = 0; i < _highlighters.Length; i++)
            {
                _highlighters[i].EndHighlight();
            }
        }

        private static IContentCachedPrefab[] GetPrefabs ()
        {
            if (_highlighterPrefabs == null)
            {
                _highlighterPrefabs = Content.Content.GetAll(PREFAB_PATH, typeof(IContentCachedPrefab)).Cast<IContentCachedPrefab>().ToArray();
            }
            return _highlighterPrefabs;
        }

        public static HighlighterCollection Create (GameObject obj)
        {
            IContentCachedPrefab[] prefabs = GetPrefabs();
            Component[] components = obj.GetComponentsInChildren<Component>();

            List<Component> comps = new List<Component>();
            List<IHighlighter> highs = new List<IHighlighter>();

            foreach (Component component in components)
            {
                var cache = prefabs.FirstOrDefault(x => x.GetCache().GetComponent<IHighlighter>().CanHighlight(component.GetType()));
                if (cache != null)
                {
                    comps.Add(component);
                    highs.Add(cache.Instantiate().GetComponent<IHighlighter>());
                }
            }

            return new HighlighterCollection(highs.ToArray(), comps.ToArray());
        }
    }
}
