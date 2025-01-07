using Common.Utils;
using Core.World;
using DG.Tweening;
using System;
using UnityEngine;

namespace Core.UnitEntities
{
    public class UnitMovement : MonoBehaviour
    {
        public event Action<IInteractable> OnStartMove;
        public event Action<IInteractable> OnEndMove;

        private const float m_MoveSpeed = 10f;

        private Transform m_Transform;
        private FloorController m_FloorController;
        private Vector2Int m_CurrentPosition;
        private IMoveProvider m_MoveProvider;

        private IInteractable m_IInteractable;

        private bool m_CanMove;

        public void Init(IMoveProvider moveProvider, FloorController floorController)
        {
            m_Transform = transform;
            m_FloorController = floorController;
            m_CurrentPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);
            m_MoveProvider = moveProvider;
            m_IInteractable = GetComponent<IInteractable>();
            Enable();
            m_MoveProvider.OnMove += Move;
        }

        private void Move(Vector2Int direction)
        {
            if (!m_CanMove)
            {
                return;
            }

            RemoveUnitFormTile();

            Result<Tile> tile = m_FloorController.TryGetTile(m_CurrentPosition + direction);

            if (!tile.IsExit)
            {
                return;
            }

            bool positionIsChanged = StartMove(tile.Object, direction);

            Vector3 newPosition = new Vector3(m_CurrentPosition.x, m_CurrentPosition.y);

            if (positionIsChanged)
            {
                m_Transform.DOMove(newPosition, 0.15f).SetEase(Ease.OutSine)
                    .OnComplete(() => EndMove(tile.Object));

            }
        }

        private void RemoveUnitFormTile()
        {
            Result<Tile> tile = m_FloorController.TryGetTile(m_CurrentPosition);

            if (tile.IsExit)
            {
                tile.Object.InteractableEntity = null;
            }
        }

        private bool StartMove(Tile tile, Vector2Int direction)
        {
            m_CanMove = false;
            bool positionIsChange = false;

            OnStartMove?.Invoke(tile.InteractableEntity);

            if (tile.InteractableEntity == null)
            {
                tile.InteractableEntity = m_IInteractable;
                m_CurrentPosition += direction;

                return true;
            }

            if (!tile.InteractableEntity.IsConsumable)
            {
                m_CurrentPosition += direction;
                positionIsChange = true;
            }

            return positionIsChange;
        }

        private void EndMove(Tile tile)
        {
            m_CanMove = true;
            if (tile.InteractableEntity == m_IInteractable)
            {
                OnEndMove?.Invoke(null);
                return;
            }

            OnEndMove?.Invoke(tile.InteractableEntity);
            tile.InteractableEntity = m_IInteractable;
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
