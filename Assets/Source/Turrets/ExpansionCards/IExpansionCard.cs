using Lomztein.BFA2.Misc;
using Lomztein.BFA2.Modification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lomztein.BFA2.Turrets.ExpansionCards
{
    public interface IExpansionCard : IIdentifiable
    {
        string Name { get; }
        string Description { get; }
        Sprite Sprite { get; }

        bool ApplyTo(IExpansionCardAcceptor target);

        bool RemoveFrom(IExpansionCardAcceptor target);

        bool CompatableWith(ModdableAttribute[] target);
    }
}
