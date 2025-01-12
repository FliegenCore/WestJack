using Core.Common;
using Core.UnitEntities;
using Core.World;
using System;
using UnityEngine;

namespace Core.PlayerExperience
{
    public class Player : MonoBehaviour, IMovable, IInteractable
    {
        private Unit m_Unit;
        private EventManager m_EventManager;

        public Unit Unit => m_Unit;

        public void Init(EventManager eventManager)
        {
            m_Unit = GetComponent<Unit>();
        }

        public void Interact(IInteractable interactable)
        {
            Debug.Log("Start fight with " + name);
            //start fight with interactable
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
            m_Unit.UnitMovement.OnMove += action;
        }

        public void UnsubscribeOnMove(Action action)
        { 
            m_Unit.UnitMovement.OnMove -= action;
        }
    }
}
