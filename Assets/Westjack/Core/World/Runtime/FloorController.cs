using Assets;
using Common;
using Common.Utils;
using Core.Common;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Core.World
{
    [Order(-200)]
    public class FloorController : MonoBehaviour, IControllerEntity
    {
        private AssetName m_AssetName = "Level_";
        
        [Inject] private AssetLoader m_AssetLoader;
        [Inject] private EventManager m_EventManager;

        private Floor m_Floor;
        private FloorData m_FloorData;

        public void PreInit()
        {
            m_FloorData = new FloorData();

            m_AssetName.Name += m_FloorData.Number;

            var floorAsset = m_AssetLoader.LoadSync<Floor>(m_AssetName.Name);
            m_Floor = m_AssetLoader.InstantiateSync(floorAsset, null);

            SpawnAnimation();
        }

        public void Init()
        {

        }

        public Result<Tile> TryGetTile(int x, int y)
        {
            Result<Tile> resTile = new Result<Tile>();
            Vector2Int pos = new Vector2Int(x, y);

            foreach (var tile in m_Floor.Tiles)
            {
                if (tile.Position == pos)
                {
                    resTile = new Result<Tile>(tile, true);

                    break;
                }
            }

            return resTile;
        }

        public Result<Tile> TryGetTile(Vector2Int pos)
        {
            return TryGetTile(pos.x, pos.y);
        }

        public void FillTile(IInteractable interactable, int x, int y)
        {
            Result<Tile> tileRes = TryGetTile(x, y);

            if (tileRes.IsExit)
            { 
                tileRes.Object.InteractableEntity = interactable;
            }
        }


        private async void SpawnAnimation()
        {
            foreach (var tile in m_Floor.Tiles)
            {
                tile.transform.localScale = Vector3.zero;
            }

            foreach (var tile in m_Floor.Tiles)
            {
                await UniTask.WaitForSeconds(0.01f);
                tile.transform.DOScale(Vector3.one, 0.25f);
            }
        }


        private void OnDestroy()
        {
            
        }
    }
}
