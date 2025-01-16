using Core.Common;
using Core.UnitEntities;
using Core.World;
using System;
using UnityEngine;

namespace Core.PlayerExperience
{
    public class Player : MonoBehaviour, IMovable, IInteractable, IDamagable, IHealthReseter
    {
        private Unit m_Unit;
        private EventManager m_EventManager;
        private bool m_WasInteract;

        public Unit Unit => m_Unit;

        public void Init(EventManager eventManager)
        {
            m_Unit = GetComponent<Unit>();
        }

        public void Interact(IInteractable interactable)
        {
            if (m_WasInteract)
            {
                return;
            }

            m_WasInteract = true;
            Debug.Log("Start fight with " + name);
        }

        public void ToggleWasInteract(bool toggle)
        {
            m_WasInteract = toggle;
        }

        public void TakeDamage(int damage)
        {
            m_Unit.Health.TakeDamage(damage);
        }

        public void AddHealth(int health)
        {
            m_Unit.Health.RestoreHealth(health);
        }

        public void EndMove(Tile tile)
        {
            if (tile.ConsumableInteractive == null)
            {
                return;
            }
            
            tile.ConsumableInteractive.Interact(this);
        }

        public void StartMove(Tile tile)
        {
            if (tile.NotConsumableInteractive == null)
            {
                return;
            }

            tile.NotConsumableInteractive.Interact(this);
        }

        public void SubscribeOnMove(Action action)
        {
            m_Unit.Movement.OnMove += action;
        }

        public void UnsubscribeOnMove(Action action)
        { 
            m_Unit.Movement.OnMove -= action;
        }
    }
}
