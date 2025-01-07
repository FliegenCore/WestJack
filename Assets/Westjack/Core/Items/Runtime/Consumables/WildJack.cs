using Core.PlayerExperience;
using UnityEngine;

namespace Core.World
{
    public class WildJack : Item
    {
        public override void Interact(IInteractable interactable)
        {
            if (interactable is not Player)
            {
                return;
            }

            Player player = interactable as Player;
            player.UnitHealth.SubscribeOnHealthChanged(Print);
            player.UnitHealth.TakeDamage(1);
            
        }

        private void Print(int health)
        {
            Debug.Log(health);
        }
    }
}
