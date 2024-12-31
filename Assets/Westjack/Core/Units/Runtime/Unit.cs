using UnityEngine;

namespace Core.UnitEntities
{
    public class Unit : MonoBehaviour
    {
        private IMoveProvider m_MoveProvider;

        public void Init(IMoveProvider moveProvider)
        {
            m_MoveProvider = moveProvider;
            m_MoveProvider.OnMove += Move;
        }

        private void Move(Vector2Int direction)
        {
            //do move 
        }

        private void OnDestroy()
        {
            m_MoveProvider.OnMove -= Move;
        }
    }
}
