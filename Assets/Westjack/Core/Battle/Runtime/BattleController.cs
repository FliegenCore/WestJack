using Assets;
using Core.Common;
using Core.PlayerExperience;
using Core.UnitEntities;
using UnityEngine;

namespace Core.Battle
{
    [Order(5)]
    public class BattleController : MonoBehaviour, IControllerEntity
    {
        [Inject] private AssetLoader m_AssetLoader;
        [Inject] private EventManager m_EventManager;
        [Inject] private CardsDeck m_DeckController;
        [Inject] private PlayerController m_PlayerController;

        private BattleUI m_BattleUI;
        private CardsDeck m_CardsDeck;

        private HandOffer m_HandOffer;

        private Enemy m_Enemy;
        private Player m_Player;

        public void PreInit()
        {
            m_BattleUI = FindAnyObjectByType<BattleUI>();

            m_CardsDeck = new CardsDeck();
            m_Player = m_PlayerController.Player;

            m_CardsDeck.CreateDeck();
            m_HandOffer = new HandOffer();

            m_EventManager.Subscribe<StartBattleSignal, Enemy>(this, StartBattle);
        }

        public void Init()
        {
            m_HandOffer.Init(m_DeckController);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            { 
                //m_HandOffer.
            }
        }

        private void StartBattle(Enemy enemy)
        {
            m_Enemy = enemy;
            //open battle ui
        }

        private void EndBattle()
        {
            
        }

        private void OnDestroy()
        {
            m_EventManager.Unsubscribe<StartBattleSignal>(this);
        }
    }
}
