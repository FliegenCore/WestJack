using UnityEngine;

namespace Core.View
{
    public class ControlMoveButton : CoreButton
    {
        [SerializeField] private Vector2Int m_Side;

        public Vector2Int Side => m_Side;
    }
}
