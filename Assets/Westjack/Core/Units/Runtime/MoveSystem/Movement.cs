using Common.Utils;
using Core.World;
using DG.Tweening;
using System;
using UnityEngine;

namespace Core.UnitEntities
{
    public class Movement : MonoBehaviour
    {
        public event Action OnMove;
        public event Action<Tile> OnStartMove;
        public event Action<Tile> OnEndMove;

        private const float m_MoveSpeed = 10f;

        private Transform m_Transform;
        private FloorController m_FloorController;
        private Vector2Int m_CurrentPosition;
        private IMoveProvider m_MoveProvider;
        private IInteractable m_IInteractable;
        private bool m_CanMove;

        public Vector2Int CurrentPosition => m_CurrentPosition;

        public void Init(IMoveProvider moveProvider)
        {
            m_Transform = transform;
            m_FloorController = moveProvider.FloorController;
            m_CurrentPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            m_MoveProvider = moveProvider;
            m_IInteractable = GetComponent<IInteractable>();
            Enable();

            m_MoveProvider.OnMove += Move;
        }

        public void Enable()
        {
            m_CanMove = true;
        }

        public void Disable()
        {
            m_CanMove = false;
        }

        private void Move(Vector2Int direction)
        {
            if (!m_CanMove)
            {
                return;
            }

            Result<Tile> tile = m_FloorController.TryGetTile(m_CurrentPosition + direction);

            if (!tile.IsExist)
            {
                return;
            }

            RemoveUnitFormTile();

            bool positionIsChanged = StartMove(tile.Object, direction);

            Vector3 newPosition = new Vector3(m_CurrentPosition.x, m_CurrentPosition.y);

            if (positionIsChanged)
            {
                m_Transform.DOMove(newPosition, 0.15f).SetEase(Ease.OutSine)
                    .OnComplete(() => EndMove(tile.Object));

                OnMove?.Invoke();
            }
        }

        private void RemoveUnitFormTile()
        {
            Result<Tile> tile = m_FloorController.TryGetTile(m_CurrentPosition);

            if (tile.IsExist)
            {
                tile.Object.NotConsumableInteractive = null;
            }
        }

        private bool StartMove(Tile tile, Vector2Int direction)
        {
            m_CanMove = false;
            bool positionIsChange = false;

            OnStartMove?.Invoke(tile);

            if (tile.NotConsumableInteractive == null)
            {
                tile.NotConsumableInteractive = m_IInteractable;
                m_CurrentPosition += direction;

                positionIsChange = true;
            }
            else
            {
                m_CanMove = true;
            }

            return positionIsChange;
        }

        private void EndMove(Tile tile)
        {
            m_CanMove = true;
            tile.NotConsumableInteractive = m_IInteractable;
            OnEndMove?.Invoke(tile);
        }

        private void OnDestroy()
        {
            if (m_MoveProvider != null)
            { 
                m_MoveProvider.OnMove -= Move;
            }
        }
    }
}
