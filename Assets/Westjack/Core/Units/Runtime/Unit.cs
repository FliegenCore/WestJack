using Core.World;
using UnityEngine;

namespace Core.UnitEntities
{
    public class Unit : MonoBehaviour
    {
        private Movement m_Movement;
        private Health m_Health;
        private IMovable m_Movable;
        private IMoveProvider m_MoveProvider;

        public Vector2Int Position
        {
            get
            {
                return m_Movement.CurrentPosition;
            }
        }

        public Health Health => m_Health;
        public Movement Movement => m_Movement;

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
            m_Movement = GetComponent<Movement>();
            m_Health = GetComponent<Health>();
            m_Movable = GetComponent<IMovable>();

            m_Movement.Init(m_MoveProvider);
            m_Health.Init();

            m_Movement.OnStartMove += m_Movable.StartMove;
            m_Movement.OnEndMove += m_Movable.EndMove;
        }


        private void OnDestroy()
        {
            m_Movement.OnStartMove -= m_Movable.StartMove;
            m_Movement.OnEndMove -= m_Movable.EndMove;
        }
    }
}
