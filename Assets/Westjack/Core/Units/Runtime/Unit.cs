using Core.World;
using UnityEngine;

namespace Core.UnitEntities
{
    public class Unit : MonoBehaviour
    {
        private UnitMovement m_UnitMovement;
        private UnitHealth m_UnitHealth;
        private IMovable m_Movable;
        private IMoveProvider m_MoveProvider;

        public Vector2Int Position
        {
            get
            {
                return new Vector2Int((int)transform.position.x, (int)transform.position.y);
            }
        }
        public UnitHealth UnitHealth => m_UnitHealth;
        public UnitMovement UnitMovement => m_UnitMovement;

        public void InitMoveProvider(FloorController floorController, IMoveProvider moveProvider = null)
        {
            if (moveProvider == null)
            {
                m_MoveProvider = GetComponent<IMoveProvider>();
                m_MoveProvider.Init(floorController);
            }
            else
            {
                m_MoveProvider = moveProvider;
                moveProvider.Init(floorController);
            }
        }

        public void Init()
        {
            m_UnitMovement = GetComponent<UnitMovement>();
            m_UnitHealth = GetComponent<UnitHealth>();
            m_Movable = GetComponent<IMovable>();

            m_UnitMovement.Init(m_MoveProvider);
            m_UnitHealth.Init();

            m_UnitMovement.OnStartMove += m_Movable.StartMove;
            m_UnitMovement.OnEndMove += m_Movable.EndMove;
        }


        private void OnDestroy()
        {
            m_UnitMovement.OnStartMove -= m_Movable.StartMove;
            m_UnitMovement.OnEndMove -= m_Movable.EndMove;
        }
    }
}
