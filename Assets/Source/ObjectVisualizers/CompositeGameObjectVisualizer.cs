using Lomztein.BFA2.ContentSystem.Objects;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.ObjectVisualizers
{
    /// <summary>
    /// Creates a set of visualizers based on the components of a GameObject, and visualizes those components accordingly.
    /// </summary>
    public class CompositeGameObjectVisualizer : ObjectVisualizerBase<GameObject>
    {
        [ModelAssetReference]
        public ObjectVisualizerSet VisualizerSet;

        private static IContentCachedPrefab[] _visualizerPrefabs;
        private const string PREFAB_PATH = "*/Visualizers/*";
        private const string VISUALIZER_PREFAB_PATH = "Prefabs/CompositeGameObjectVisualizer";

        private Dictionary<Component, IEnumerable<IObjectVisualizer>> _highlighters;

        private Dictionary<Component, IEnumerable<IObjectVisualizer>> GenerateVisualizersFor(GameObject obj)
        {
            var prefabs = GetPrefabs(VisualizerSet);
            Component[] components = obj.GetComponentsInChildren<Component>();
            Dictionary<Component, IEnumerable<IObjectVisualizer>> visualizers = new Dictionary<Component, IEnumerable<IObjectVisualizer>>();

            foreach (Component component in components)
            {
                List<IObjectVisualizer> list = new List<IObjectVisualizer>();

                var caches = prefabs.Where(x => x.GetCache().GetComponent<IObjectVisualizer>().CanVisualize(component));
                foreach (var cache in caches)
                {
                    GameObject visualizerObj = cache.Instantiate();
                    visualizerObj.transform.position = Vector3.zero;
                    visualizerObj.transform.SetParent(transform, false);

                    list.Add(visualizerObj.GetComponent<IObjectVisualizer>());
                }

                if (list.Count > 0)
                {
                    visualizers.Add(component, list);
                }
            }

            return visualizers;
        }

        public override void Visualize (GameObject obj)
        {
            if (_highlighters == null)
            {
                _highlighters = GenerateVisualizersFor(obj);
            }

            foreach (var pair in _highlighters)
            {
                foreach (var highlighter in pair.Value)
                {
                    highlighter.Visualize(pair.Key);
                }
            }
        }

        public override void EndVisualization()
        {
            base.EndVisualization();
            foreach (var pair in _highlighters)
            {
                foreach (var highlighter in pair.Value)
                {
                    highlighter.EndVisualization();
                }
            }
            Destroy(gameObject);
        }

        public override void Tint (Color color)
        {
            foreach (var pair in _highlighters)
            {
                foreach (var highlighter in pair.Value)
                {
                    highlighter.Tint(color);
                }
            }
        }

        private static IEnumerable<IContentCachedPrefab> GetPrefabs (ObjectVisualizerSet set)
        {
            if (_visualizerPrefabs == null)
            {
                _visualizerPrefabs = ContentSystem.Content.GetAll(PREFAB_PATH, typeof(IContentCachedPrefab)).Cast<IContentCachedPrefab>().ToArray();
            }
            return _visualizerPrefabs.Where(x => set.Contains(x.GetCache().GetComponent<IObjectVisualizer>().Identifier));
        }

        public static CompositeGameObjectVisualizer CreateFrom (ObjectVisualizerSet set)
        {
            GameObject highlighterCollectionObj = Instantiate(Resources.Load<GameObject>(VISUALIZER_PREFAB_PATH));
            var visualizer = highlighterCollectionObj.GetComponent<CompositeGameObjectVisualizer>();
            visualizer.VisualizerSet = set;
            return visualizer;
        }
    }
}
