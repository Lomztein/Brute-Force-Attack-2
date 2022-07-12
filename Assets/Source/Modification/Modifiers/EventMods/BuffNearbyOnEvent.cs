using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Modifiers.ModBroadcasters;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.EventMods
{
    [CreateAssetMenu(fileName = "New " + nameof(BuffNearbyOnEvent), menuName = "BFA2/Mods/Buff Nearby On Event")]
    public class BuffNearbyOnEvent : ModBase
    {
        [ModelAssetReference]
        public EventInfo Event;
        [ModelAssetReference]
        public Mod BuffMod;
        [ModelAssetReference]
        public StatInfo BuffTimeInfo;
        [ModelAssetReference]
        public StatInfo BuffCoeffecientInfo;
        [ModelAssetReference]
        public StatInfo BuffRangeInfo;
        [ModelProperty]
        public float BuffTime = -1;
        [ModelProperty]
        public float BuffTimePerStack;
        [ModelProperty]
        public int BuffCoeffecient;
        [ModelProperty]
        public int BuffCoeffecientPerStack;
        [ModelProperty]
        public int BuffRange;
        [ModelProperty]
        public int BuffRangePerStack;

        private IStatReference _time;
        private IStatReference _coeffecient;
        private IStatReference _range;

        public override void ApplyBase(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            events.GetEvent(Event.Identifier).Event.AddListener(x => OnEvent(x, moddable), this);
            _time = stats.AddStat(BuffTimeInfo, BuffTime, this);
            _coeffecient = stats.AddStat(BuffCoeffecientInfo, BuffCoeffecient, this);
            _range = stats.AddStat(BuffRangeInfo, BuffRange, this);
        }

        public override void ApplyStack(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            stats.AddStatElement(BuffTimeInfo.Identifier, new StatElement(this, BuffTimePerStack), this);
            stats.AddStatElement(BuffCoeffecientInfo.Identifier, new StatElement(this, BuffCoeffecientPerStack), this);
            stats.AddStatElement(BuffRangeInfo.Identifier, new StatElement(this, BuffRangePerStack), this);
        }

        public override void RemoveBase(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            events.GetEvent(Event.Identifier).Event.RemoveListener(x => OnEvent(x, moddable), this);
            stats.RemoveStat(BuffTimeInfo.Identifier, this);
            stats.RemoveStat(BuffCoeffecientInfo.Identifier, this);
            stats.RemoveStat(BuffRangeInfo.Identifier, this);
        }

        public override void RemoveStack(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            stats.RemoveStatElement(BuffTimeInfo.Identifier, this, this);
            stats.RemoveStatElement(BuffCoeffecientInfo.Identifier, this, this);
            stats.RemoveStatElement(BuffRangeInfo.Identifier, this, this);
        }

        private void OnEvent (EventArgs args, IModdable source)
        {
            if (source is Component component)
            {
                BuffModBroadcaster.AddBuffsInArea(BuffMod, component.transform.position, _range.GetValue(), _time.GetValue());
            }
        }
    }
}
