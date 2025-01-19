using UnityEngine;

namespace Core.Battle
{
    public class BattleUI : MonoBehaviour
    {
        [SerializeField] private SelectCardsView m_SelectCardView;
        [SerializeField] private HandView m_PlayerHandView;
        [SerializeField] private HandView m_EnemyHandView;

        public SelectCardsView SelectCardView => m_SelectCardView;
        public HandView PlayerHandView => m_PlayerHandView;
        public HandView EnemyHandView => m_EnemyHandView;

    }
}
