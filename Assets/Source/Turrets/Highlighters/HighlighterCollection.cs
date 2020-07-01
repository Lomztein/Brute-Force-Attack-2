using Lomztein.BFA2.Content.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.Highlighters
{
    public class HighlighterCollection : MonoBehaviour
    {
        private static IContentCachedPrefab[] _highlighterPrefabs;
        private const string PREFAB_PATH = "*/Highlighters";
        private const string COLLECTION_PREFAB_PATH = "Prefabs/HighlighterCollectionPrefab";

        private IHighlighter[] _highlighters;
        private Component[] _components;

        public void Init (IHighlighter[] highlighters, Component[] components)
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
            Destroy(gameObject);
        }

        public void Tint (Color color)
        {
            foreach (IHighlighter highlighter in _highlighters)
            {
                highlighter.Tint(color);
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
            GameObject highlighterCollectionObj = Instantiate(Resources.Load<GameObject>(COLLECTION_PREFAB_PATH));

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

                    GameObject highlighterObj = cache.Instantiate();
                    highlighterObj.transform.position = Vector3.zero;
                    highlighterObj.transform.SetParent(highlighterCollectionObj.transform, false);

                    highs.Add(highlighterObj.GetComponent<IHighlighter>());
                }
            }

            HighlighterCollection highlighterCollection = highlighterCollectionObj.GetComponent<HighlighterCollection>();
            highlighterCollection.Init(highs.ToArray(), comps.ToArray());
            return highlighterCollection;
        }
    }
}
