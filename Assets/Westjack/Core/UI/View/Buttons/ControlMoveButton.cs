using System;
using UnityEngine;

namespace Core.UI
{
    public class ControlMoveButton : CoreButton
    {
        public event Action<Vector2Int> OnClick;

        [SerializeField] private Vector2Int m_Side;

        protected override void Click()
        {
            OnClick?.Invoke(m_Side);
        }
    }
}
