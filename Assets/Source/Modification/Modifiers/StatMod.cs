using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers
{
    public class StatMod : ModBase, IMod
    {
        [ModelProperty]
        public Element[] Stats = new Element[0];

        [Serializable]
        public class Element
        {
            [ModelProperty]
            public StatInfo Info;
            [ModelProperty]
            public float Value = 0;
        }

        public override void ApplyBase(IStatContainer stats, IEventContainer events)
        {
            foreach (Element element in Stats)
            {
                stats.AddStatElement(element.Info.Identifier, new StatElement(element, element.Value));
            }
        }

        public override void ApplyStack(IStatContainer stats, IEventContainer events)
        {
            foreach (Element element in Stats)
            {
                stats.AddStatElement(element.Info.Identifier, new StatElement(element, element.Value));
            }
        }

        public override void RemoveBase(IStatContainer stats, IEventContainer events)
        {
            foreach (Element element in Stats)
            {
                stats.RemoveStatElement(element.Info.Identifier, element);
            }
        }

        public override void RemoveStack(IStatContainer stats, IEventContainer events)
        {
            foreach (Element element in Stats)
            {
                stats.RemoveStatElement(element.Info.Identifier, element);
            }
        }

        public override string ToString()
        {
            return string.Join("\n", Stats.Select(x => x.ToString()));
        }
    }
}
