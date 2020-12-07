using System;

namespace Lomztein.BFA2.Modification.ModBroadcasters.ExpansionCards
{
    public interface IExpansionCardContainer
    {
        int Capacity { get; set; }
        IExpansionCard[] CurrentCards { get; }

        event Action<IExpansionCard> OnCardAdded;
        event Action<IExpansionCard> OnCardRemoved;

        void AddCard(IExpansionCard card);
        bool RemoveCard(IExpansionCard card);
    }
}