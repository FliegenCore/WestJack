using System;
using UnityEngine;

namespace Core.UnitEntities
{
    public class JoystickController : MonoBehaviour, IMoveProvider, IControllerEntity
    {
        public event Action<Vector2Int> OnMove;

        public void PreInit()
        {
        }

        public void Init()
        {
        }

        private void OnDestroy()
        {
            
        }
    }
}
