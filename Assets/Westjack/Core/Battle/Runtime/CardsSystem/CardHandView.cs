using DG.Tweening;
using UnityEngine;

namespace Core.Battle
{
    public class CardHandView : CardView
    {
        //TODO: on hold show info
        private RectTransform m_Transform;

        private float m_PositionX;

        public CardHandView SetX(float x)
        {
            m_PositionX = x;

            return this;
        }

        public CardHandView SetSpawnPositionX(float x)
        {
            if (m_Transform == null)
            {
                m_Transform = GetComponent<RectTransform>();
            }

            m_Transform.anchoredPosition = new Vector2(x, m_Transform.anchoredPosition.y);

            return this;
        }

        public CardHandView Enable()
        {
            gameObject.SetActive(true);

            return this;
        }

        public void Disable()
        {
            gameObject.SetActive(false);    
        }

        public void Shift()
        {
            if (m_Transform == null)
            {
                m_Transform = GetComponent<RectTransform>();
            }

            transform.DOLocalMoveX(m_Transform.anchoredPosition.x + m_PositionX, 0.25f);
        }
    }
}
