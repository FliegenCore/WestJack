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
        [Inject] private PlayerController m_PlayerController;

        private BattleUI m_BattleUI;
        private CardsDeck m_CardsDeck;

        private SelectCardOffer m_SelectOffer;

        private Hand m_PlayerHand;
        private Hand m_EnemyHand;

        private Enemy m_Enemy;
        private Player m_Player;

        public void PreInit()
        {
            m_BattleUI = FindFirstObjectByType<BattleUI>();

            m_CardsDeck = new CardsDeck();
            m_CardsDeck.CreateDeck();

            m_Player = m_PlayerController.Player;

            m_SelectOffer = new SelectCardOffer();

            m_SelectOffer.Init(m_CardsDeck);

            m_PlayerHand = new Hand();
            m_EnemyHand = new Hand();

            m_PlayerHand.Init();
            m_EnemyHand.Init();

            m_BattleUI.PlayerHandView.Init();
            m_BattleUI.EnemyHandView.Init();
        }

        public void Init()
        {
            m_EventManager.Subscribe<StartBattleSignal, Enemy>(this, StartBattle);

            m_SelectOffer.OnCardAddedInOffer += m_BattleUI.SelectCardView.UpdateCards;
            m_BattleUI.SelectCardView.SubscribeOnCardSelect(m_PlayerHand.TakeCard);

            m_PlayerHand.OnTakeCard += m_BattleUI.PlayerHandView.AddCard;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                m_SelectOffer.UpdateCards();
            }
        }

        private void StartBattle(Enemy enemy)
        {
            m_Enemy = enemy;

            //open battle ui
            //take 2 card for player and enemy
        }

        private void EndBattle()
        {
            
        }

        private void OnDestroy()
        {
            m_SelectOffer.OnCardAddedInOffer -= m_BattleUI.SelectCardView.UpdateCards;
            m_EventManager.Unsubscribe<StartBattleSignal>(this);
        }
    }
}
