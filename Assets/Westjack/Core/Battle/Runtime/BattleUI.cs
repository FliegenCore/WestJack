using UnityEngine;

namespace Core.Battle
{
    public class BattleUI : MonoBehaviour
    {
        [SerializeField] private SelectCardView m_SelectCardView;
        [SerializeField] private HandView m_PlayerHandView;
        [SerializeField] private HandView m_EnemyHandView;

        public SelectCardView SelectCardView => m_SelectCardView;
        public HandView PlayerHandView => m_PlayerHandView;
        public HandView EnemyHandView => m_EnemyHandView;

    }
}
