using UnityEngine;

namespace Core.World
{
    public class Tile : MonoBehaviour
    {
        public IInteractable InteractableEntity;

        public Vector2Int Position { 
            get 
            {
                return new Vector2Int((int)transform.position.x, (int)transform.position.y);
            }
        }
    }
}
