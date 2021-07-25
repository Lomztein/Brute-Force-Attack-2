using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Modifiers.ModBroadcasters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Highlighters
{
    public class AreaModBroadcasterHighlighter : HighlighterBase<AreaModBroadcaster>
    {
        public GameObject IndicatorPrefab;
        private List<GameObject> _currentRenderers = new List<GameObject>();

        private ModBroadcaster _component;
        private Color _tint;

        public override void Highlight(AreaModBroadcaster component)
        {
            _component = component;
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
            ClearLineRenderers();
            if (_component)
            {
                IEnumerable<IModdable> targets = _component.GetBroadcastTargets();
                var uniqueParents = targets.Where(x => x is Component).Cast<Component>().Select(x => x.transform.root).Distinct();
                foreach (Transform parent in uniqueParents)
                {
                    Structure structure = parent.GetComponent<Structure>();
                    if (structure)
                    {
                        float x = World.Grid.SizeOf(structure.Width) * 1.5f;
                        float y = World.Grid.SizeOf(structure.Height) * 1.5f;
                        GameObject indicator = Instantiate(IndicatorPrefab);
                        indicator.GetComponent<SpriteRenderer>().color = _tint;
                        indicator.transform.localScale = new Vector3(x, y, 1);
                        indicator.transform.position = structure.transform.position;
                        _currentRenderers.Add(indicator);
                    }
                }
            }
        }

        public override void EndHighlight()
        {
            base.EndHighlight();
            ClearLineRenderers();
        }

        private void ClearLineRenderers ()
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

        public override void Tint(Color color)
        {
            _tint = color;
        }
    }
}
