using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.BFA2.Modification.ModBroadcasters.ExpansionCards
{
    public class ExpansionCardContainer : IExpansionCardContainer
    {
        public int Capacity { get; set; }

        public event Action<IExpansionCard> OnCardAdded;
        public event Action<IExpansionCard> OnCardRemoved;

        public IExpansionCard[] CurrentCards => _expansionCards.ToArray();

        private List<IExpansionCard> _expansionCards = new List<IExpansionCard>();

        public void AddCard(IExpansionCard card)
        {
            _expansionCards.Add(card);
            OnCardAdded?.Invoke(card);
        }

        public bool RemoveCard(IExpansionCard card)
        {
            bool removed = _expansionCards.Remove(card);
            if (removed)
            {
                OnCardRemoved?.Invoke(card);
            }
            return removed;
        }
    }
}