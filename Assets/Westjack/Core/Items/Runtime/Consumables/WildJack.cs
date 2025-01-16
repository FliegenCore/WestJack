using Core.PlayerExperience;
using Core.UnitEntities;
using System;
using UnityEngine;

namespace Core.World
{
    public class WildJack : Item
    {
        public new event Action<IInteractable> OnTake;

        public override void Interact(IInteractable interactable)
        {
            IHealthReseter health = (IHealthReseter) interactable;

            if (health != null)
            {
                health.AddHealth(1);
                OnTake?.Invoke(null);
            }
        }

        private void Print(int health)
        {
            Debug.Log(health);
        }
    }
}
