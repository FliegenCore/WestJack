using Assets;
using Common;
using Core.UnitEntities;
using UnityEngine;

namespace Core.PlayerExperience
{
    [Order(-1)]
    public class PlayerController : MonoBehaviour, IControllerEntity
    {
        [Inject] private JoystickController m_Joystick;
        [Inject] private AssetLoader m_AssetLoader;

        private readonly AssetName m_PlayerPrefabName = "Player";

        private Player m_Player;

        public Transform GetPlayerTransform()
        {
            return m_Player.transform;
        }

        public void PreInit()
        {
            var playerAsset = m_AssetLoader.LoadSync<Player>(m_PlayerPrefabName);
            m_Player = m_AssetLoader.InstantiateSync(playerAsset, null);
        }

        public void Init()
        {
            m_Player.Init(m_Joystick);
        }

       
    }

}
