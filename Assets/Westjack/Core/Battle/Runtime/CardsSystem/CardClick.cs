using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Battle
{
    public class CardClick : MonoBehaviour, IPointerClickHandler
    {
        public event Action<Card> OnCardChoose;
        public event Action OnClick;

        private Card m_Card;

        private bool m_CanTake;

        public void SetCard(Card card)
        {
            m_Card = card;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!m_CanTake)
            {
                return;
            }

            m_CanTake = false;
            OnCardChoose?.Invoke(m_Card);
            OnClick?.Invoke();
            DoAnim();
        }

        public void DisableInteractble()
        {
            m_CanTake = false;
        }

        public void EnableInteractble()
        {
            m_CanTake = true;
        }

        private void DoAnim()
        {

        }
    }
}
