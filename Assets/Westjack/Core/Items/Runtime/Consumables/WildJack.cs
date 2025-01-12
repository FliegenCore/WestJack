using Core.PlayerExperience;
using Core.UnitEntities;
using System;
using UnityEngine;

namespace Core.World
{
    public class WildJack : Item
    {
        public override event Action<IInteractable> OnTake;

        public override void Interact(IInteractable interactable)
        {
            if (interactable is not Player)
            {
                return;
            }

            Player player = interactable as Player;
            player.Unit.UnitHealth.SubscribeOnHealthChanged(Print);
            player.Unit.UnitHealth.TakeDamage(1);
            OnTake?.Invoke(null);
        }

        private void Print(int health)
        {
            Debug.Log(health);
        }
    }
}
