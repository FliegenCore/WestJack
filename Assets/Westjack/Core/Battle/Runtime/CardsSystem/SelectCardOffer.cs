using Common.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Battle
{
    public class SelectCardOffer
    {
        public event Action<List<Card>> OnCardAddedInOffer;

        private List<Card> m_Cards;
        private CardsDeck m_CardsDeck;

        public void Init(CardsDeck cardsDeck)
        {
            m_Cards = new List<Card>();
            m_CardsDeck = cardsDeck;
        }

        public void UpdateCards()
        {
            m_Cards.Clear();
            List<Card> cards = m_CardsDeck.TakeCards(2);

            if (cards.Count <= 0)
            {
                return;
            }

            OnCardAddedInOffer(cards);
        }
    }
}
