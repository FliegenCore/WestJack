using System;
using UnityEngine;

namespace Core.World
{
    public abstract class Item : MonoBehaviour, IInteractable
    {
        public event Action<IInteractable> OnTake;

        public Vector2Int Position
        {
            get
            {
                return new Vector2Int((int)transform.position.x, (int)transform.position.y);
            }
        }

        public bool IsConsumable { get; set; }

        public abstract void Interact(IInteractable intareactable);

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
