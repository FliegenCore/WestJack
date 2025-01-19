using DG.Tweening;
using UnityEngine;

namespace Core.Battle
{
    public class CardHandView : CardView
    {
        //TODO: on hold show info
        private RectTransform m_Transform;
        private float m_PosX;

        public RectTransform Transform => m_Transform;

        public void Init()
        { 
            m_Transform = GetComponent<RectTransform>();
        }

        public CardHandView Enable()
        {
            gameObject.SetActive(true);

            return this;
        }

        public CardHandView Disable()
        {
            gameObject.SetActive(false);

            return this;
        }

        public void SetOffsetX(float offset)
        { 
            m_PosX += offset;
        }

        public void Shift()
        {
            transform.DOLocalMoveX(m_PosX, 0.25f);
        }
    }
}
