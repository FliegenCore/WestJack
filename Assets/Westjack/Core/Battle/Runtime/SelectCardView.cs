using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Battle
{
    public class SelectCardView : MonoBehaviour
    {
        [SerializeField] private CardBattleView[] m_CardsView;

        public void UpdateCards(List<Card> cards)
        {
            for (int i = 0; i < m_CardsView.Length; i++)
            {
                SubscribeOnCardClick(DisableChoose);
                m_CardsView[i].CardClick.SetCard(cards[i]);
                m_CardsView[i].SetRank((int)cards[i].CardRank);
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

        private void RotateCards()
        { 
            
        }
    }
}
