using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Battle
{
    public class HandView : MonoBehaviour
    {
        public event Action OnCardTake;

        [SerializeField] private CardHandView[] m_Cards;

        private CardHandView m_Card;
        private int m_ActiveCardsCount;
        private float m_NextCardPosition;
        private float m_CardSizeX;

        public void Init()
        {
            Image image = m_Cards[0].GetComponent<Image>();
            m_CardSizeX = image.rectTransform.sizeDelta.x;

            for (int i = 0; i < m_Cards.Length; i++)
            {
                m_Cards[i].Init();
            }
        }

        public void AddCard(Card card, int index)
        {
            m_Cards[index - 1]
                .SetRank((int)card.CardRank)
                .SetType(card.CardType);

            m_Card = m_Cards[index - 1];

            ShiftCards();
            TakeCardAnim();

            m_ActiveCardsCount = index;

            m_NextCardPosition += m_CardSizeX / 2.5f;
        }

        private void ShiftCards()
        {
            for (int i = 0; i < m_ActiveCardsCount; i++)
            {
                m_Cards[i].SetOffsetX(-m_CardSizeX / 2.5f);
            }

            if (m_ActiveCardsCount <= 0)
            {
                return;
            }

            for (int i = 0; i < m_ActiveCardsCount; i++)
            {
                m_Cards[i].Shift();
            }
        }

        private void TakeCardAnim()
        {
            m_Card.Enable();
            m_Card.SetOffsetX(m_NextCardPosition);

            m_Card.Transform.DOLocalMoveX(m_NextCardPosition, 0.25f)
                .OnComplete(() =>OnCardTake?.Invoke());
        }
    }
}
