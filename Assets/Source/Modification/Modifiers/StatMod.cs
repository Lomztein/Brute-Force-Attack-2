﻿using System;
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
    [CreateAssetMenu (fileName = "NewStatMod", menuName = "BFA2/Mods/Stat Mod")]
    public class StatMod : ModBase
    {
        [ModelProperty]
        public Element[] Stats = new Element[0];

        [Serializable]
        public class Element
        {
            [ModelAssetReference]
            public StatInfo Info;
            [ModelProperty]
            public float Value = 0;
        }

        public override void ApplyBase(IStatContainer stats, IEventContainer events)
        {
            foreach (Element element in Stats)
            {
                stats.AddStatElement(element.Info.Identifier, new StatElement(element, element.Value * Coeffecient), this);
            }
        }

        public override void ApplyStack(IStatContainer stats, IEventContainer events)
        {
            foreach (Element element in Stats)
            {
                stats.AddStatElement(element.Info.Identifier, new StatElement(element, element.Value * Coeffecient), this);
            }
        }

        public override void RemoveBase(IStatContainer stats, IEventContainer events)
        {
            foreach (Element element in Stats)
            {
                stats.RemoveStatElement(element.Info.Identifier, element, this);
            }
        }

        public override void RemoveStack(IStatContainer stats, IEventContainer events)
        {
            foreach (Element element in Stats)
            {
                stats.RemoveStatElement(element.Info.Identifier, element, this);
            }
        }

        public override string ToString()
        {
            return string.Join("\n", Stats.Select(x => x.ToString()));
        }
    }
}
