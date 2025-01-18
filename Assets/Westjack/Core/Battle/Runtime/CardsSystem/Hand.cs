using System;
using System.Collections.Generic;

namespace Core.Battle
{
    public class Hand 
    {
        public event Action<Card, int> OnTakeCard;
        private List<Card> m_Cards;

        public int CardCount => m_Cards.Count;
        
        public void Init()
        {
            m_Cards = new List<Card>();
        }

        public void TakeCard(Card card)
        {
            m_Cards.Add(card);
            OnTakeCard?.Invoke(card, CardCount);
        }

        private void SumCards()
        { 
            
        }
    }
}
