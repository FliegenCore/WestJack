using Assets;
using Common;
using Common.Utils;
using UnityEngine;

namespace Core.World
{
    [Order(-50)]
    public class ItemsController : MonoBehaviour, IControllerEntity
    {
        private readonly AssetName m_AssetName = "Item";

        [Inject] private FloorController m_FloorController;
        [Inject] private AssetLoader m_AssetLoader;

        private Item m_Item;

        public void PreInit()
        {
            var itemAsset = m_AssetLoader.LoadSync<Item>(m_AssetName);
            Result<Tile> tileRes = m_FloorController.TryGetTile(2,2);
            if (tileRes.IsExit)
            { 
                m_Item = m_AssetLoader.InstantiateSync(itemAsset, tileRes.Object.transform);
                m_FloorController.FillTile(m_Item, 2, 2);
            }
        }

        public void Init()
        {
        }

        private void OnDestroy()
        {
            
        }
    }

}
