using System;
using UnityEngine;

namespace Core.World
{
    public class Item : MonoBehaviour, IInteractable
    {
        public event Action OnTake;

        public bool IsEnemy { get; set; }

        public void Interact()
        {
            OnTake?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
