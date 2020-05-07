using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Modifiers.ModProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Turrets.ExpansionCards
{
    public interface IExpansionCardAcceptor
    {
        bool HasCapacity();

        bool InsertCard(IExpansionCard card);

        bool RemoveCard(IExpansionCard card);

        ModdableAttribute[] Attributes { get; }
    }
}
