using Common.Utils;
using System;
using System.Collections.Generic;

namespace Core.Battle
{
    public class HandOffer
    {
        public event Action<Card> OnCardAddedInOffer;
        public event Action<Card> OnCardRemovedWithoutOffer;

        private List<Card> m_Cards;
        private CardsDeck m_DeckController;

        public void Init(CardsDeck deckController)
        {
            m_Cards = new List<Card>();
            m_DeckController = deckController;
        }

        public void AddCard()
        {
            Result<Card> card = m_DeckController.TakeCard();

            if (card.IsExist)
            { 
                OnCardAddedInOffer?.Invoke(card.Object);
            }
        }

        public void RemoveCard()
        {
            Result<Card> card = m_DeckController.TakeCard();

            if (card.IsExist)
            {
                OnCardRemovedWithoutOffer?.Invoke(card.Object);
            }
        }
    }
}
