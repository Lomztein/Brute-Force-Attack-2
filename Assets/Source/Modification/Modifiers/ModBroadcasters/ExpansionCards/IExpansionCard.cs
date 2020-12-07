using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Modifiers;
using Lomztein.BFA2.Modification.Modifiers.ModBroadcasters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Modification.ModBroadcasters.ExpansionCards
{
    public interface IExpansionCard : IIdentifiable
    {
        string Name { get; }
        string Description { get; }
        Sprite Sprite { get; }
    }
}
