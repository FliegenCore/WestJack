using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Battle
{
    public class SelectCardsView : MonoBehaviour
    {
        [SerializeField] private CardSelectView[] m_CardsView;

        public void Init()
        {
            SubscribeOnCardClick(DisableChoose);

            foreach (var card in m_CardsView)
            {
                card.OnTurnBack += card.Apply;
                card.OnTurnFront += card.CardClick.EnableInteractble;
            }
        }

        public void InitCards()
        {
            foreach (var card in m_CardsView)
            {
                card.Init();
            }
        }

        public void StartSelect()
        {
            foreach (var card in m_CardsView)
            {
                card.SetSide(CardSide.Back);
            }
        }

        public void UpdateCards(List<Card> cards)
        {
            for (int i = 0; i < m_CardsView.Length; i++)
            {
                m_CardsView[i].CardClick.SetCard(cards[i]); 
                m_CardsView[i].SetRank((int)cards[i].CardRank);
                m_CardsView[i].TurnOverToFront();
            }
        }

        public void SubscribeOnCardSelect(Action<Card> action)
        {
            for (int i = 0; i < m_CardsView.Length; i++)
            {
                m_CardsView[i].CardClick.OnCardChoose += action;
            }
        }

        public void UnsubscribeOnCardSelect(Action<Card> action)
        {
            for (int i = 0; i < m_CardsView.Length; i++)
            {
                m_CardsView[i].CardClick.OnCardChoose -= action;
            }
        }

        public void SubscribeOnCardClick(Action action)
        {
            for (int i = 0; i < m_CardsView.Length; i++)
            {
                m_CardsView[i].CardClick.OnClick += action;
            }
        }

        public void UnsubscribeOnCardClick(Action action)
        {
            for (int i = 0; i < m_CardsView.Length; i++)
            {
                m_CardsView[i].CardClick.OnClick -= action;
            }
        }

        private void DisableChoose()
        {
            for (int i = 0; i < m_CardsView.Length; i++)
            {
                m_CardsView[i].CardClick.DisableInteractble();
            }
        }
    }
}
