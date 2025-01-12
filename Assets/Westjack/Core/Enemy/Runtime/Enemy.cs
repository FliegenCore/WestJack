using Core.World;
using UnityEngine;

namespace Core.UnitEntities
{
    public class Enemy : MonoBehaviour, IInteractable, IMovable
    {
        private Unit m_Unit;
        private IMoveProvider m_MoveProvider; 

        public Unit Unit => m_Unit;

        public void Init()
        {
            m_Unit = GetComponent<Unit>();
        }

        public void Interact(IInteractable intareactable)
        {
            Debug.Log("Start fight with " + name);
        }

        public void EndMove(Tile tile)
        {

        }

        public void StartMove(Tile tile)
        {
            if (tile.NotConsumableInteractive == null)
            {
                return;
            }

            tile.NotConsumableInteractive.Interact(this);
        }
    }
}
