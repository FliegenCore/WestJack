using UnityEngine;

namespace Core.UI
{
    public class CoreUI : MonoBehaviour
    {
        [SerializeField] private BottomCoreUI m_BottomCoreUI;
        [SerializeField] private TopCoreUI m_TopCoreUI;

        public BottomCoreUI BottomCoreUI => m_BottomCoreUI;
        public TopCoreUI TopCoreUI => m_TopCoreUI;
    }
}
