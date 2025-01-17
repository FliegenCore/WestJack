using UnityEngine;

namespace Core.Battle
{
    public class BattleUI : MonoBehaviour
    {
        [SerializeField] private SelectCardView m_SelectCardView;
        [SerializeField] private HandCardView m_PlayerHandView;
        [SerializeField] private HandCardView m_EnemyHandView;

        public SelectCardView SelectCardView => m_SelectCardView;
        public HandCardView PlayerHandView => m_PlayerHandView;
        public HandCardView EnemyHandView => m_EnemyHandView;

    }
}
