using Common.Utils;
using System.Collections.Generic;

namespace Core.Battle
{
    public class CardsDeck
    {
        private List<Card> m_Cards = new List<Card>();

        public IEnumerable<Card> Cards => m_Cards;
        public int m_CardCount => m_Cards.Count;

        public void CreateDeck()
        {
            foreach (CardType suit in System.Enum.GetValues(typeof(CardType)))
            {
                foreach (CardRank rank in System.Enum.GetValues(typeof(CardRank)))
                {
                    m_Cards.Add(new Card(suit, rank));
                }
            }

            Shuffle();
        }

        public Result<Card> TakeCard()
        { 
            Result<Card> result = new Result<Card>();

            if (m_Cards.Count > 0)
            {
                Card card = m_Cards[0];
                m_Cards.RemoveAt(0);
                result = new Result<Card>(card, true);
            }

            return result;
        }

        public List<Card> TakeCards(int count)
        {
            List<Card> cards = new List<Card>();

            for (int i = 0; i < count; i++)
            {
                Result<Card> cardRes = TakeCard();

                if (cardRes.IsExist)
                {
                    cards.Add(cardRes.Object);
                }
            }

            return cards;
        }

        private void Shuffle()
        {
            System.Random rng = new System.Random();
            int n = m_Cards.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = m_Cards[k];
                m_Cards[k] = m_Cards[n];
                m_Cards[n] = value;
            }
        }
    }
}
