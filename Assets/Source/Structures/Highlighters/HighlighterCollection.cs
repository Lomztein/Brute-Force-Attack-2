using Lomztein.BFA2.ContentSystem.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Highlighters
{
    public class HighlighterCollection : MonoBehaviour
    {
        private static IContentCachedPrefab[] _highlighterPrefabs;
        private const string PREFAB_PATH = "*/Highlighters";
        private const string COLLECTION_PREFAB_PATH = "Prefabs/HighlighterCollectionPrefab";

        private Dictionary<Component, IEnumerable<IHighlighter>> _highlighters;

        public void Init (Dictionary<Component, IEnumerable<IHighlighter>> highlighters)
        {
            _highlighters = highlighters;
        }

        public void Highlight ()
        {
            foreach (var pair in _highlighters)
            {
                foreach (var highlighter in pair.Value)
                {
                    highlighter.Highlight(pair.Key);
                }
            }
        }

        public void EndHighlight()
        {
            foreach (var pair in _highlighters)
            {
                foreach (var highlighter in pair.Value)
                {
                    highlighter.EndHighlight();
                }
            }
            Destroy(gameObject);
        }

        public void Tint (Color color)
        {
            foreach (var pair in _highlighters)
            {
                foreach (var highlighter in pair.Value)
                {
                    highlighter.Tint(color);
                }
            }
        }

        private static IEnumerable<IContentCachedPrefab> GetPrefabs (HighlighterSet set)
        {
            if (_highlighterPrefabs == null)
            {
                _highlighterPrefabs = ContentSystem.Content.GetAll(PREFAB_PATH, typeof(IContentCachedPrefab)).Cast<IContentCachedPrefab>().ToArray();
            }
            return _highlighterPrefabs.Where(x => set.Contains(x.GetCache().GetComponent<IHighlighter>().Identifier));
        }

        public static HighlighterCollection Create (GameObject obj, HighlighterSet set)
        {
            GameObject highlighterCollectionObj = Instantiate(Resources.Load<GameObject>(COLLECTION_PREFAB_PATH));

            var prefabs = GetPrefabs(set);
            Component[] components = obj.GetComponentsInChildren<Component>();
            Dictionary<Component, IEnumerable<IHighlighter>> highlighters = new Dictionary<Component, IEnumerable<IHighlighter>>();

            foreach (Component component in components)
            {
                List<IHighlighter> list = new List<IHighlighter>();

                var caches = prefabs.Where(x => x.GetCache().GetComponent<IHighlighter>().CanHighlight(component.GetType()));
                foreach (var cache in caches)
                {
                    GameObject highlighterObj = cache.Instantiate();
                    highlighterObj.transform.position = Vector3.zero;
                    highlighterObj.transform.SetParent(highlighterCollectionObj.transform, false);

                    list.Add(highlighterObj.GetComponent<IHighlighter>());
                }

                highlighters.Add(component, list);
            }

            HighlighterCollection highlighterCollection = highlighterCollectionObj.GetComponent<HighlighterCollection>();
            highlighterCollection.Init(highlighters);
            return highlighterCollection;
        }
    }
}
