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
        int ExpansionCardCapacity { get; }
        IExpansionCard[] ExpansionCards { get; }

        bool InsertExpansionCard(IExpansionCard card);

        bool RemoveExpansionCard(IExpansionCard card);

        ModdableAttribute[] Attributes { get; }
    }

    public static class ExpansionCardAcceptorExtensions
    {
        public static bool AtExpansionCardCapacity(this IExpansionCardAcceptor acceptor) => acceptor.ExpansionCards.Length >= acceptor.ExpansionCardCapacity;
    }
}
