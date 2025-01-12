using Assets;
using Common;
using Common.Utils;
using Core.Common;
using Core.PlayerExperience;
using Core.World;
using Unity.VisualScripting;
using UnityEngine;

namespace Core.UnitEntities
{
    [Order(10)]
    public class EnemyController : MonoBehaviour, IControllerEntity
    {
        [Inject] private AssetLoader m_AssetLoader;
        [Inject] private EventManager m_EventManager;
        [Inject] private FloorController m_FloorController;
        [Inject] private PlayerController m_PlayerController;

        private AssetName m_AssetName = "Enemy_";
        
        private Enemy m_Enemy;

        public void PreInit()
        {
            LoadEnemy();
        }

        public void Init()
        {
            
        }

        private void LoadEnemy()
        {
            var enemyAsset = m_AssetLoader.LoadSync<Enemy>(m_AssetName);

            Result<Tile> tileRes = m_FloorController.TryGetTile(1, 1);

            if (tileRes.IsExist)
            {
                m_Enemy = m_AssetLoader.InstantiateSync(enemyAsset, tileRes.Object.transform);
                m_FloorController.FillNotConsumableTile(m_Enemy, 1, 1);

                m_Enemy.Init();

                m_Enemy.Unit.InitMoveProvider(m_FloorController);
                m_Enemy.Unit.Init();
            }
        }

        private void OnDestroy()
        {
            
        }
    }
}
