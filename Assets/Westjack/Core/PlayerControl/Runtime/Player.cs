using Core.UnitEntities;
using Core.World;
using UnityEngine;

namespace Core.PlayerExperience
{
    public class Player : MonoBehaviour, IInteractable
    {
        private UnitMovement m_UnitMovement;
        private UnitHealth m_UnitHealth;

        public Vector2Int Position
        {
            get
            {
                return new Vector2Int((int)transform.position.x, (int)transform.position.y);
            }
        }
        public UnitHealth UnitHealth => m_UnitHealth;

        public bool IsConsumable { get; set; }

        public void Init(IMoveProvider moveProvider, FloorController floorController)
        {
            IsConsumable = true;
            m_UnitMovement = GetComponent<UnitMovement>();
            m_UnitHealth = GetComponent<UnitHealth>();

            m_UnitMovement.Init(moveProvider, floorController);
            m_UnitMovement.OnStartMove += InteractWithEntityOnStart;
            m_UnitMovement.OnEndMove += InteractWithEntityOnEnd;

            m_UnitHealth.Init();
        }

        private void InteractWithEntityOnStart(Tile tile)
        {
            if (tile.NotConsumableInteractive == null)
            {
                return;
            }

            tile.NotConsumableInteractive.Interact(this);
        }

        private void InteractWithEntityOnEnd(Tile tile)
        {
            if (tile.ConsumableInteractive == null)
            {
                return;
            }

            tile.ConsumableInteractive.Interact(this); 
        }

        public void Interact(IInteractable intareactable)
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
