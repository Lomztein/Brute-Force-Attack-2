﻿using Lomztein.BFA2.Modification;
using Lomztein.BFA2.Modification.Modifiers.ModBroadcasters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.BFA2.Modification.ModBroadcasters.ExpansionCards
{
    public interface IExpansionCardAcceptor
    {
        IExpansionCardContainer ExpansionCards { get; }

        bool IsCompatableWith(IExpansionCard card);
    }

    public static class ExpansionCardAcceptorExtensions
    {
        public static bool AtExpansionCardCapacity(this IExpansionCardAcceptor acceptor) => acceptor.ExpansionCards.CurrentCards.Length >= acceptor.ExpansionCards.Capacity;
    }
}
