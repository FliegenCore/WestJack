using UnityEngine;

namespace Core.Battle
{
    public class HandView : MonoBehaviour
    {
        [SerializeField] private CardHandView[] m_Cards;

        public CardHandView[] Cards => m_Cards;

        private int m_ActiveCardsCount;
        private float m_NextCardPosition;

        public void AddCard(Card card, int index)
        {
            m_Cards[index - 1].SetRank((int)card.CardRank)
                .SetType(card.CardType);

            m_Cards[index - 1].Enable();

            m_ActiveCardsCount = index - 1;
        }

        private void ShiftCards()
        {
            if (m_ActiveCardsCount <= 0)
            {
                return;
            }

            for (int i = 0; i < m_ActiveCardsCount; i++)
            {
                m_Cards[i].Shift(-100f);
            }
        }
    }
}
