using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.Modifiers.EventMods
{
    [CreateAssetMenu(fileName = "New " + nameof(DebugLogOnEvent), menuName = "BFA2/Mods/Debug Log On Event")]
    public class DebugLogOnEvent : ModBase
    {
        [ModelAssetReference]
        public EventInfo EventInfo;
        [ModelProperty]
        public string Message;

        public override void ApplyBase(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            events.GetEvent(EventInfo.Identifier).Event.AddListener(Event_OnExecute, this);
        }

        private void Event_OnExecute(Events.EventArgs obj)
        {
            Debug.Log($"Event {Name}: {Message}");
        }

        public override void ApplyStack(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
        }

        public override void RemoveBase(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            events.GetEvent(EventInfo.Identifier).Event.RemoveListener(Event_OnExecute, this);
        }

        public override void RemoveStack(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
        }
    }
}
