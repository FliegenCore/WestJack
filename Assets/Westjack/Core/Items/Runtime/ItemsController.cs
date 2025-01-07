using Assets;
using Common;
using Common.Utils;
using Core.Common;
using Core.PlayerExperience;
using Core.UnitEntities;
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

        private WildJack m_Item;

        public void PreInit()
        {
            var itemAsset = m_AssetLoader.LoadSync<WildJack>(m_AssetName);
            Result<Tile> tileRes = m_FloorController.TryGetTile(2,2);
            if (tileRes.IsExit)
            { 
                m_Item = m_AssetLoader.InstantiateSync(itemAsset, tileRes.Object.transform);
                m_FloorController.FillTile(m_Item, 2, 2);
                m_Item.OnTake += _ => ResetItem();
            }
        }

        private void ResetItem()
        {
            m_EventManager.TriggerEvenet<ResetTileSignal, Vector2Int>(m_Item.Position);
            m_Item.Disable();
        }

        public void Init()
        {
        }

        private void OnDestroy()
        {
            m_Item.OnTake -= _ => ResetItem();
        }
    }

}
