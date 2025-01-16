using Core.World;
using UnityEngine;

namespace Core.UnitEntities
{
    public class Enemy : MonoBehaviour, IInteractable, IMovable, IDamagable
    {
        private Unit m_Unit;
        private IMoveProvider m_MoveProvider;

        private bool m_WasInteract;

        public Unit Unit => m_Unit;

        public void Init()
        {
            m_Unit = GetComponent<Unit>();
        }

        public void Interact(IInteractable intareactable)
        {
            if (m_WasInteract)
            {
                return;
            }

            m_WasInteract = true;
            Debug.Log("Start fight with " + name);
        }

        public void TakeDamage(int damage)
        {
            m_Unit.Health.TakeDamage(damage);
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
