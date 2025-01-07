using Assets;
using Common;
using Common.Utils;
using Core.Common;
using UnityEngine;

namespace Core.World
{
    [Order(-50)]
    public class ItemsController : MonoBehaviour, IControllerEntity
    {
        private readonly AssetName m_AssetName = "WildJack";

        [Inject] private FloorController m_FloorController;
        [Inject] private AssetLoader m_AssetLoader;
        [Inject] private EventManager m_EventManager;

        private WildJack m_WildJack;

        public void PreInit()
        {
            var itemAsset = m_AssetLoader.LoadSync<WildJack>(m_AssetName);
            Result<Tile> tileRes = m_FloorController.TryGetTile(2,2);
            if (tileRes.IsExit)
            { 
                m_WildJack = m_AssetLoader.InstantiateSync(itemAsset, tileRes.Object.transform);
                m_FloorController.FillConsumableTile(m_WildJack, 2, 2);
            }

            m_WildJack.OnTake += _ => ResetItem();
        }

        private void ResetItem()
        {
            m_EventManager.TriggerEvenet<ResetTileSignal, Vector2Int>(m_WildJack.Position);
            m_WildJack.Disable();
            Debug.Log("ResetItem");
        }

        public void Init()
        {
        }

        private void OnDestroy()
        {
            m_WildJack.OnTake -= _ => ResetItem();
        }
    }
}
