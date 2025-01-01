using System;
using UnityEngine;

namespace Core.UnitEntities
{
    public class UnitMovement : MonoBehaviour
    {
        public event Action OnMoveEnd;

        private const float m_MoveSpeed = 10f;

        private IMoveProvider m_MoveProvider;

        private Vector2Int m_CurrentPosition;
        private Transform m_Transform;

        private bool m_IsMoveEnd;

        public void Init(IMoveProvider moveProvider)
        {
            m_Transform = transform;

            m_CurrentPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            m_MoveProvider = moveProvider;
            m_MoveProvider.OnMove += StartMove;
            OnMoveEnd += Print;
        }

        private void Print()
        {
            Debug.Log("End move");
        }

        private void StartMove(Vector2Int direction)
        {
            m_CurrentPosition += direction;
            m_IsMoveEnd = false;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (m_IsMoveEnd)
            {
                return;
            }

            Vector2Int playerPosition = new Vector2Int((int)m_Transform.position.x, (int)m_Transform.position.y);

            float multiplyer = m_MoveSpeed * Time.deltaTime;

            Vector2 newPosition = new Vector2(m_CurrentPosition.x, m_CurrentPosition.y);

            m_Transform.position = Vector3.MoveTowards(m_Transform.position, newPosition, multiplyer);

            if (playerPosition == m_CurrentPosition)
            {
                OnMoveEnd?.Invoke();
                m_IsMoveEnd = true;
            }
        }

        private void OnDestroy()
        {
            m_MoveProvider.OnMove -= StartMove;
        }
    }
}
