using Assets;
using Common;
using UnityEngine;

namespace Core.UI
{
    [Order(-100)]
    public class CoreUIController : MonoBehaviour, IControllerEntity
    {
        [Inject] private AssetLoader m_AssetLoader;

        private AssetName m_CoreCanvasName = "CoreUI";
        private CoreUI m_CoreUI;

        public CoreUI CoreCanvas => m_CoreUI;

        public void PreInit()
        {
            var coreCanvasAsset = m_AssetLoader.LoadSync<CoreUI>(m_CoreCanvasName);
            m_CoreUI = m_AssetLoader.InstantiateSync(coreCanvasAsset, null);
        }

        public void Init()
        {
        }

        public BottomCoreUI GetBottomUI()
        {
            return m_CoreUI.BottomCoreUI;
        }
    }
}
