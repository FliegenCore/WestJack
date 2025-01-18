using UnityEngine;
using UnityEngine.UI;

namespace Core.Battle
{
    public class HandView : MonoBehaviour
    {
        [SerializeField] private CardHandView[] m_Cards;

        public CardHandView[] Cards => m_Cards;

        private int m_ActiveCardsCount;
        private float m_NextCardPosition;
        private float m_CardSizeX;

        public void Init()
        {
            Image image = m_Cards[0].GetComponent<Image>();

            m_CardSizeX = image.rectTransform.sizeDelta.x;
        }

        public void AddCard(Card card, int index)
        {
            m_Cards[index - 1].SetSpawnPositionX(m_NextCardPosition)
                .SetX(-m_CardSizeX / 2)
                .SetRank((int)card.CardRank)
                .SetType(card.CardType);

            m_Cards[index - 1].Enable();

            m_ActiveCardsCount = index;

            ShiftCards();
        }

        private void ShiftCards()
        {
            if (m_ActiveCardsCount <= 1)
            {
                return;
            }

            for (int i = 0; i < m_ActiveCardsCount; i++)
            {
                m_Cards[i].Shift();
            }

            m_NextCardPosition += m_CardSizeX / 2.5f;
        }
    }
}
