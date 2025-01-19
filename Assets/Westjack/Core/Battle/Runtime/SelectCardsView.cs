using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Battle
{
    public class SelectCardsView : MonoBehaviour
    {
        public event Action OnCardsTurnAway;
        public event Action OnCardsTurnAround;

        [SerializeField] private CardSelectView[] m_CardsView;

        public void UpdateCards(List<Card> cards)
        {
            TurnCardsAway(() =>
            {
                for (int i = 0; i < m_CardsView.Length; i++)
                {
                    SubscribeOnCardClick(DisableChoose);
                    m_CardsView[i].CardClick.SetCard(cards[i]);
                    m_CardsView[i].SetRank((int)cards[i].CardRank);
                }
            });
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

        private async void TurnCardsAway(Action action)
        {
            for (int i = 0; i < m_CardsView.Length; i++)
            {
                m_CardsView[i].transform.DORotate(new Vector3(0, 90, 0), 0.25f);
            }

            await UniTask.WaitForSeconds(0.25f);

            OnCardsTurnAway?.Invoke();

            action?.Invoke();

            TurnCardsAround();
        }

        private async void TurnCardsAround()
        {
            for (int i = 0; i < m_CardsView.Length; i++)
            {
                m_CardsView[i].transform.DORotate(new Vector3(0, 0, 0), 0.25f);
            }

            await UniTask.WaitForSeconds(0.25f);

            OnCardsTurnAround?.Invoke();
        }

    }
}
