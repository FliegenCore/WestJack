using DG.Tweening;
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

        private bool m_CanMove;

        public void Init(IMoveProvider moveProvider)
        {
            m_Transform = transform;

            m_CurrentPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            m_MoveProvider = moveProvider;
            Enable();
            m_MoveProvider.OnMove += Move;
        }

        private void Move(Vector2Int direction)
        {
            if (!m_CanMove)
            {
                return;
            }
            m_CanMove = false;

            m_CurrentPosition += direction;

            Vector3 newPosition = new Vector3(m_CurrentPosition.x, m_CurrentPosition.y);

            m_Transform.DOMove(newPosition, 0.15f).SetEase(Ease.OutSine).OnComplete(EndMove);
        }

        private void EndMove()
        {
            m_CanMove = true;
            OnMoveEnd?.Invoke();
        }

        public void Enable()
        {
            m_CanMove = true;
        }

        public void Disable()
        {
            m_CanMove = false;
        }

        private void OnDestroy()
        {
            m_MoveProvider.OnMove -= Move;
        }
    }
}
