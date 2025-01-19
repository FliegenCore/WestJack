using UnityEngine;

namespace Core.Battle
{
    public class CardSelectView : CardView
    {
        [SerializeField] private CardClick m_CardClick;

        public CardClick CardClick => m_CardClick;
    }
}
