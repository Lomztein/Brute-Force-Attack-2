using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Modifiers.ModBroadcasters;
using Lomztein.BFA2.Structures;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.ObjectVisualizers
{
    public class AdjacencyModBroadcasterVisualizer : ObjectVisualizerBase<AdjacencyModBroadcaster>
    {
        public GameObject IndicatorPrefab;
        private List<GameObject> _currentRenderers = new List<GameObject>();

        private AdjacencyModBroadcaster _component;
        private Color _tint = Color.white;

        public override void Visualize(AdjacencyModBroadcaster component)
        {
            Follow(component.transform);
            _component = component;
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
            ClearRenderers();
            if (_component)
            {
                IEnumerable<IModdable> targets = _component.GetBroadcastTargets().ToArray();
                var structures = targets.Cast<Structure>();
                var thisStructure = _component.GetComponent<Structure>();
                foreach (var structure in structures)
                {
                    float xRatio = World.Grid.SizeOf(thisStructure.Width) / (World.Grid.SizeOf(thisStructure.Width) + World.Grid.SizeOf(structure.Width));
                    float yRatio = World.Grid.SizeOf(thisStructure.Height) / (World.Grid.SizeOf(thisStructure.Height) + World.Grid.SizeOf(structure.Height));
                    float x = Mathf.Lerp(thisStructure.transform.position.x, structure.transform.position.x, xRatio);
                    float y = Mathf.Lerp(thisStructure.transform.position.y, structure.transform.position.y, yRatio);
                    GameObject indicator = Instantiate(IndicatorPrefab);
                    indicator.GetComponentInChildren<SpriteRenderer>().color = _tint;
                    indicator.transform.position = new Vector3(x, y, transform.position.z - 1f);

                    float angle = Mathf.Rad2Deg * Mathf.Atan2(structure.transform.position.y - _component.transform.position.y, structure.transform.position.x - _component.transform.position.x);
                    indicator.transform.rotation = Quaternion.Euler(0f, 0f, angle);

                    _currentRenderers.Add(indicator);
                }
            }
        }

        private void ClearRenderers()
        {
            foreach (GameObject renderer in _currentRenderers)
            {
                if (renderer)
                {
                    Destroy(renderer);
                }
            }
            _currentRenderers.Clear();
        }

        public override void EndVisualization()
        {
            base.EndVisualization();
            ClearRenderers();
        }

        public override void Tint(Color color)
        {
            _tint = color;
        }
    }
}
