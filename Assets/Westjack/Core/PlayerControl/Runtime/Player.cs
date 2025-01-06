using Core.UnitEntities;
using Core.World;
using UnityEngine;

namespace Core.PlayerExperience
{
    public class Player : MonoBehaviour, IInteractable
    {
        private UnitMovement m_UnitMovement;

        public Vector2Int Position
        {
            get
            {
                return new Vector2Int((int)transform.position.x, (int)transform.position.y);
            }
        }

        public bool IsEnemy { get; set; }

        public void Init(IMoveProvider moveProvider, FloorController floorController)
        {
            IsEnemy = true;
            m_UnitMovement = GetComponent<UnitMovement>();
            m_UnitMovement.Init(moveProvider, floorController);
            m_UnitMovement.OnStartMove += InteractWithEntityOnStart;
            m_UnitMovement.OnEndMove += InteractWithEntityOnEnd;
        }

        private void InteractWithEntityOnStart(IInteractable interactable)
        {
            if (interactable == null)
            {
                return;
            }

            if (interactable.IsEnemy)
            {
                interactable.Interact();
            }
        }

        private void InteractWithEntityOnEnd(IInteractable interactable)
        {
            if (interactable == null)
            {
                return;
            }

            interactable.Interact();  
        }

        public void Interact()
        {
            Debug.Log("Player");
        }

        private void OnDestroy()
        {
            m_UnitMovement.OnStartMove -= InteractWithEntityOnStart;
            m_UnitMovement.OnEndMove -= InteractWithEntityOnEnd;
        }
    }
}
