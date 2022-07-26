using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Modifiers.ModBroadcasters;
using Lomztein.BFA2.Structures;
using Lomztein.BFA2.Structures.StructureManagement;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.BFA2.ObjectVisualizers
{
    public class AreaModBroadcasterRecieverVisualizer : ObjectVisualizerBase<IModdable>
    {
        public GameObject RangerPrefab;
        public GameObject StructurePrefab;
        public bool OnlyShowWithinRange;

        private IModdable _component;

        private Dictionary<AreaModBroadcaster, LocalHighlighter> _localHighlighters = new Dictionary<AreaModBroadcaster, LocalHighlighter>();

        public override void Visualize(IModdable component)
        {
            _component = component;
            if (_component is Component comp)
            {
                Follow(comp.transform);
            }

            IEnumerable<AreaModBroadcaster> broadcasters = StructureManager.GetStructures().Select(x => x.GetComponentInChildren<AreaModBroadcaster>()).Where(x => x != null);
            foreach (AreaModBroadcaster broadcaster in broadcasters)
            {
                bool show = OnlyShowWithinRange ? IsWithinRange(broadcaster) : true;
                if (broadcaster.Mod.CanMod(component) && show)
                {
                    RangerVisualizer rh = Instantiate(RangerPrefab, broadcaster.transform.position, Quaternion.identity, transform).GetComponent<RangerVisualizer>();
                    StructureVisualizer sh = Instantiate(StructurePrefab, broadcaster.transform.position, Quaternion.identity, transform).GetComponent<StructureVisualizer>();
                    LocalHighlighter highlighter = new LocalHighlighter(rh, sh);
                    highlighter.Visualize(broadcaster);
                    _localHighlighters.Add(broadcaster, highlighter);
                }
            }
        }

        private bool IsWithinRange(AreaModBroadcaster broadcaster)
        {
            Component component = _component as Component;
            if (broadcaster.Mod.CanMod(_component))
            {
                return component.transform.root.GetComponent<Structure>().OverlapsCircle(broadcaster.transform.position, broadcaster.GetRange());
            }
            else
            {
                return false;
            }
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);
            foreach (var pair in _localHighlighters)
            {
                pair.Value.SetStructureActive(IsWithinRange(pair.Key));
            }
        }

        public override void EndVisualization()
        {
            base.EndVisualization();
            foreach (var pair in _localHighlighters)
            {
                pair.Value.EndVisualization();
            }
        }

        public override void Tint(Color color)
        {
            foreach (var pair in _localHighlighters)
            {
                pair.Value.Tint(color);
            }
        }

        private class LocalHighlighter
        {
            public RangerVisualizer Range;
            public StructureVisualizer Structure;

            public LocalHighlighter(RangerVisualizer range, StructureVisualizer structure)
            {
                Range = range;
                Structure = structure;
            }

            public void Visualize (AreaModBroadcaster broadcaster)
            {
                Range.Visualize((Component)broadcaster);
                Structure.Visualize((Component)broadcaster.GetComponent<Structure>());
            }

            public void SetStructureActive(bool value) => Structure.gameObject.SetActive(value);

            public void EndVisualization ()
            {
                Range.EndVisualization();
                Structure.EndVisualization();
            }

            public void Tint(Color color)
            {
                Range.Tint(color);
                Structure.Tint(color);
            }
        }
    }
}
