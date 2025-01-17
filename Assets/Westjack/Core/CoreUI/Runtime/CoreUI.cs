using UnityEngine;

namespace Core.UI
{
    public class CoreUI : MonoBehaviour
    {
        [SerializeField] private BottomCoreUI m_BottomCoreUI;
        [SerializeField] private TopCoreUI m_TopCoreUI;
        [SerializeField] private MiddleCoreUI m_MiddleCoreUI;

        public BottomCoreUI BottomCoreUI => m_BottomCoreUI;
        public TopCoreUI TopCoreUI => m_TopCoreUI;
        public MiddleCoreUI MiddleCoreUI => m_MiddleCoreUI;

    }
}
