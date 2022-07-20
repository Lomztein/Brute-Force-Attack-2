using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Events;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Stats;
using Lomztein.BFA2.Purchasing.Resources;
using Lomztein.BFA2.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2
{
    public class BonusResourceOnEvent : ModBase
    {
        [ModelAssetReference]
        public EventInfo Event;

        [ModelAssetReference]
        public Resource Resource;

        public override void ApplyBase(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            events.GetEvent(Event.Identifier).Event.AddListener(OnEvent, this);
        }

        public override void ApplyStack(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveBase(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveStack(IModdable moddable, IStatContainer stats, IEventContainer events)
        {
            throw new System.NotImplementedException();
        }

        private void OnEvent (EventArgs args)
        {
        }
    }
}
