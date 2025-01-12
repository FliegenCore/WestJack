using UnityEngine;

namespace Core.UI
{
    public class CoreUI : MonoBehaviour
    {
        [SerializeField] private BottomCoreUI m_BottomCoreUI;

        public BottomCoreUI BottomCoreUI => m_BottomCoreUI;
    }
}
