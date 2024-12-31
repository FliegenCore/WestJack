using Core.View;
using UnityEngine;

namespace Core.Controls
{
    public class JoystickView : MonoBehaviour
    {
        [SerializeField] private ControlMoveButton[] m_ControlButtons;

        public ControlMoveButton[] ControlButtons => m_ControlButtons;
    }
}
