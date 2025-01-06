using System;
using UnityEngine;

namespace Core.World
{
    public class Item : MonoBehaviour, IInteractable
    {
        public event Action<IInteractable> OnTake;

        public bool IsEnemy { get; set; }

        public void Interact(IInteractable intareactable)
        {
            OnTake?.Invoke(intareactable);
            gameObject.SetActive(false);
        }
    }
}
