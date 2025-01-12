using Assets;
using Common;
using Common.Utils;
using Core.Common;
using Core.UnitEntities;
using Core.World;
using System;
using UnityEngine;

namespace Core.PlayerExperience
{
    [Order(-1)]
    public class PlayerController : MonoBehaviour, IControllerEntity
    {
        private readonly AssetName m_PlayerPrefabName = "Player";

        [Inject] private JoystickController m_Joystick;
        [Inject] private AssetLoader m_AssetLoader;
        [Inject] private FloorController m_FloorController;
        [Inject] private EventManager m_EventManager;

        private Player m_Player;

        public Transform GetPlayerTransform()
        {
            return m_Player.transform;
        }

        public void PreInit()
        {
            var playerAsset = m_AssetLoader.LoadSync<Player>(m_PlayerPrefabName);
            Result<Tile> tileRes = m_FloorController.TryGetTile(0,0);

            if(tileRes.IsExist)
            {
                m_Player = m_AssetLoader.InstantiateSync(playerAsset, tileRes.Object.transform);

                m_Player.Init(m_EventManager);

                m_FloorController.FillNotConsumableTile(m_Player, m_Player.Unit.Position.x, m_Player.Unit.Position.y);
            }
        }

        public void Init()
        {
            m_Player.Unit.InitMoveProvider(m_FloorController, m_Joystick);
            m_Player.Unit.Init();
        }

        public void SubscribeOnPlayerMove(Action action)
        {
            m_Player.SubscribeOnMove(action);
        }

        public void UnsubscribeOnPlayerMove(Action action)
        {
            m_Player.UnsubscribeOnMove(action);
        }
    }
}
