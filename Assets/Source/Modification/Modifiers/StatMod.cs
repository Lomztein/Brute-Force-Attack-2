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
    public class StatMod : BaseModComponent, IMod
    {
        [ModelProperty]
        public Element[] Stats;

        [Serializable]
        public class Element
        {
            public string Identifier;
            public string Name;
            public string Description;

            public Stat.Type Type;
            public float Value;
        }

        public override void ApplyBase(IStatContainer stats, IEventContainer events)
        {
            foreach (Element element in Stats)
            {
                if (!stats.HasStat (element.Identifier))
                {
                    stats.AddStat(element.Identifier, element.Name, element.Description);
                }
                stats.AddStatElement(element.Identifier, new StatElement(element, element.Value), element.Type);
            }
        }

        public override void ApplyStack(IStatContainer stats, IEventContainer events)
        {
            foreach (Element element in Stats)
            {
                stats.AddStatElement(element.Identifier, new StatElement(element, element.Value), element.Type);
            }
        }

        public override void RemoveBase(IStatContainer stats, IEventContainer events)
        {
            foreach (Element element in Stats)
            {
                stats.RemoveStatElement(element.Identifier, element, element.Type);
            }
        }

        public override void RemoveStack(IStatContainer stats, IEventContainer events)
        {
            foreach (Element element in Stats)
            {
                stats.RemoveStatElement(element.Identifier, element, element.Type);
            }
        }
    }
}
