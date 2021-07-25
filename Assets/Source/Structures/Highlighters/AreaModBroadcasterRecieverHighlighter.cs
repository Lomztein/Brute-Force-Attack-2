using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Modifiers.ModBroadcasters;
using Lomztein.BFA2.Structures.StructureManagement;
using Lomztein.BFA2.Structures.Turrets.Rangers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Structures.Highlighters
{
    public class AreaModBroadcasterRecieverHighlighter : HighlighterBase<IModdable>
    {
        public GameObject RangerPrefab;
        public GameObject StructurePrefab;
        public bool OnlyShowWithinRange;

        private IModdable _component;

        private Dictionary<AreaModBroadcaster, LocalHighlighter> _localHighlighters = new Dictionary<AreaModBroadcaster, LocalHighlighter>();

        public override void Highlight(IModdable component)
        {
            _component = component;

            IEnumerable<AreaModBroadcaster> broadcasters = StructureManager.GetStructures().Select(x => x.GetComponentInChildren<AreaModBroadcaster>()).Where(x => x != null);
            foreach (AreaModBroadcaster broadcaster in broadcasters)
            {
                bool show = OnlyShowWithinRange ? IsWithinRange(broadcaster) : true;
                if (broadcaster.Mod.CanMod(component) && show)
                {
                    RangerHighlighter rh = Instantiate(RangerPrefab, broadcaster.transform.position, Quaternion.identity, transform).GetComponent<RangerHighlighter>();
                    StructureHighlighter sh = Instantiate(StructurePrefab, broadcaster.transform.position, Quaternion.identity, transform).GetComponent<StructureHighlighter>();
                    LocalHighlighter highlighter = new LocalHighlighter(rh, sh);
                    highlighter.Highlight(broadcaster);
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

        public override void EndHighlight()
        {
            base.EndHighlight();
            foreach (var pair in _localHighlighters)
            {
                pair.Value.EndHighlight();
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
            public RangerHighlighter Range;
            public StructureHighlighter Structure;

            public LocalHighlighter(RangerHighlighter range, StructureHighlighter structure)
            {
                Range = range;
                Structure = structure;
            }

            public void Highlight (AreaModBroadcaster broadcaster)
            {
                Range.Highlight((Component)broadcaster);
                Structure.Highlight((Component)broadcaster.GetComponent<Structure>());
            }

            public void SetStructureActive(bool value) => Structure.gameObject.SetActive(value);

            public void EndHighlight ()
            {
                Range.EndHighlight();
                Structure.EndHighlight();
            }

            public void Tint(Color color)
            {
                Range.Tint(color);
                Structure.Tint(color);
            }
        }
    }
}
