using Assets;
using Common;
using Common.Utils;
using Core.Common;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LightTransport.IProbeIntegrator;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Core.World
{
    [Order(-200)]
    public class FloorController : MonoBehaviour, IControllerEntity
    {
        private AssetName m_AssetName = "Level_";
        
        [Inject] private AssetLoader m_AssetLoader;
        [Inject] private EventManager m_EventManager;

        private Dictionary<Vector2Int, Tile> m_Tiles = new Dictionary<Vector2Int, Tile>();
        private Floor m_Floor;
        private FloorData m_FloorData;

        public void PreInit()
        {
            m_FloorData = new FloorData();

            m_AssetName.Name += m_FloorData.Number;

            var floorAsset = m_AssetLoader.LoadSync<Floor>(m_AssetName.Name);
            m_Floor = m_AssetLoader.InstantiateSync(floorAsset, null);
        }

        public void Init()
        {
            m_EventManager.Subscribe<ResetConsumableTileSignal, Vector2Int>(this, ResetConsumableTile);
            m_EventManager.Subscribe<ResetNotConsumableTileSignal, Vector2Int>(this, ResetNotConsumableTile);

            FillTilesDictionary();
            SpawnAnimation();
        }

        public Result<Tile> TryGetTile(int x, int y)
        {
            Result<Tile> resTile = new Result<Tile>();
            Vector2Int pos = new Vector2Int(x, y);

            if (m_Tiles.ContainsKey(pos))
            {
                resTile = new Result<Tile>(m_Tiles[pos], true);
            }

            return resTile;
        }

        public Result<Tile> TryGetTile(Vector2Int pos)
        {
            return TryGetTile(pos.x, pos.y);
        }

        public Result<Tile> FindNearestTile(Vector2Int current, Vector2Int target)
        {
            Result<Tile> nearestTile = new Result<Tile>();

            //TODO fix allocations
            List<Result<Tile>> neighbors = new List<Result<Tile>>
            {
                TryGetTile(current.x + 1, current.y),
                TryGetTile(current.x - 1, current.y),
                TryGetTile(current.x, current.y + 1),
                TryGetTile(current.x, current.y - 1)
            };

            float distance;
            float minDistanceSquared = float.PositiveInfinity;

            foreach (var neighbor in neighbors)
            {
                if (!neighbor.IsExist)
                {
                    continue;
                }

                distance = (neighbor.Object.Position - target).sqrMagnitude;

                if (distance < minDistanceSquared)
                {
                    minDistanceSquared = distance;
                    nearestTile = neighbor;
                }
            }

            return nearestTile;
        }

        public void FillConsumableTile(IInteractable interactable, int x, int y)
        {
            Result<Tile> tileRes = TryGetTile(x, y);

            if (tileRes.IsExist)
            { 
                tileRes.Object.ConsumableInteractive = interactable;
            }
        }

        public void FillNotConsumableTile(IInteractable interactable, int x, int y)
        {
            Result<Tile> tileRes = TryGetTile(x, y);

            if (tileRes.IsExist)
            {
                tileRes.Object.NotConsumableInteractive = interactable;
            }
        }

        private void FillTilesDictionary()
        {
            foreach (var tile in m_Floor.Tiles)
            {
                m_Tiles.Add(tile.Position, tile);
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

            if (tileRes.IsExist)
            {
                tileRes.Object.ConsumableInteractive = null;
            }
        }

        private void ResetNotConsumableTile(Vector2Int position)
        {
            Result<Tile> tileRes = TryGetTile(position);

            if (tileRes.IsExist)
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
            m_EventManager.Unsubscribe<ResetConsumableTileSignal>(this);
            m_EventManager.Unsubscribe<ResetNotConsumableTileSignal>(this);
        }
    }
}
