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
            m_EventManager.Subscribe<ResetTileSignal, Vector2Int>(this, ResetConsumableTile);
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

        public void FillConsumableTile(IInteractable interactable, int x, int y)
        {
            Result<Tile> tileRes = TryGetTile(x, y);

            if (tileRes.IsExit)
            { 
                tileRes.Object.ConsumableInteractive = interactable;
            }
        }

        public void FillNotConsumableTile(IInteractable interactable, int x, int y)
        {
            Result<Tile> tileRes = TryGetTile(x, y);

            if (tileRes.IsExit)
            {
                tileRes.Object.NotConsumableInteractive = interactable;
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

        private void ResetConsumableTile(Vector2Int position)
        {
            Result<Tile> tileRes = TryGetTile(position);

            if (tileRes.IsExit)
            {
                tileRes.Object.ConsumableInteractive = null;
            }
        }

        private void ResetNotConsumableTile(Vector2Int position)
        {
            Result<Tile> tileRes = TryGetTile(position);

            if (tileRes.IsExit)
            {
                tileRes.Object.NotConsumableInteractive = null;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                foreach (var tile in m_Floor.Tiles)
                {
                    if (tile.ConsumableInteractive == null && tile.NotConsumableInteractive == null)
                    {
                        continue;
                    }
                    Debug.Log($"{tile.Position} | {tile.ConsumableInteractive} | {tile.NotConsumableInteractive}");
                }
            }
        }

        private void OnDestroy()
        {
            m_EventManager.Unsubscribe<ResetTileSignal>(this);
        }
    }
}
