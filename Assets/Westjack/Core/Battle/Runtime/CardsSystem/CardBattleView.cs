using UnityEngine;

namespace Core.Battle
{
    public class CardBattleView : CardView
    {
        [SerializeField] private CardClick m_CardClick;

        public CardClick CardClick => m_CardClick;
    }
}
