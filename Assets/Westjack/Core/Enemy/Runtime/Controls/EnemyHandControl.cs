using Core.PlayerExperience;
using Core.World;
using System;
using UnityEngine;

namespace Core.UnitEntities
{
    public class EnemyHandControl : MonoBehaviour, IMoveProvider, ITurned
    {
        public event Action<Vector2Int> OnMove;

        private Player m_Player;
        private FloorController m_FloorController;
        private Unit m_Unit;

        public FloorController FloorController => m_FloorController;

        public void Init(FloorController floorController)
        {
            m_Player = FindFirstObjectByType<Player>();
            m_Unit = GetComponent<Unit>();
            m_FloorController = floorController;
        }

        public void DoTurn()
        {
            //choose side 
            Vector2Int side = new Vector2Int(1, 0);

            

            OnMove?.Invoke(side);
        }
    }
}
