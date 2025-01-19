using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Battle
{
    public enum CardSide
    { 
        Back,
        Front
    }

    public class CardView : MonoBehaviour
    {
        public event Action OnTurnFront;
        public event Action OnTurnBack;

        [SerializeField] private TMP_Text m_RankText;
        [SerializeField] private Image m_CardBack;

        private CardSide m_CardSide;
        private CardType m_CardType;
        private int m_Rank;

        public void Init()
        {
            OnTurnBack?.Invoke();
        }

        public void SetSide(CardSide side)
        {
            m_CardSide = side;

            if (side == CardSide.Back)
            {
                m_CardBack.gameObject.SetActive(true);
                OnTurnBack?.Invoke();
            }
            else if(side == CardSide.Front)
            {
                m_CardBack.gameObject.SetActive(false);
                OnTurnFront?.Invoke();
            }
        }

        public CardView TurnOverToFront()
        {
            if (m_CardSide == CardSide.Front)
            {
                TurnBack(() =>
                {
                    TurnAround();
                });
            }
            else if (m_CardSide == CardSide.Back)
            {
                TurnAround();
            }

            return this;
        }

        public CardView TurnOver(Action action = null)
        {
            if (m_CardSide == CardSide.Front)
            {
                TurnBack();
            }
            else if(m_CardSide == CardSide.Back)
            { 
                TurnAround();
            }

            action?.Invoke();

            return this;
        }

        public CardView SetType(CardType type)
        {
            return this;
        }

        public CardView SetRank(int rank)
        {
            m_Rank = rank;

            return this;
        }

        public void Apply()
        { 
            m_RankText.text = m_Rank.ToString();
        }

        private void TurnBack(Action action = null)
        {
            transform.DORotate(new Vector3(0, 90, 0), 0.25f)
                .OnComplete(() =>
                {
                    m_CardBack.gameObject.SetActive(true);
                    transform.DORotate(new Vector3(0, 0, 0), 0.25f).OnComplete(() => { 
                        OnTurnBack?.Invoke();
                        action?.Invoke();
                    });
                });

            m_CardSide = CardSide.Back;
        }

        private void TurnAround(Action action = null)
        {
            transform.DORotate(new Vector3(0, 90, 0), 0.25f)
                .OnComplete(() =>
                {
                    m_CardBack.gameObject.SetActive(false);
                    transform.DORotate(new Vector3(0, 0, 0), 0.25f).OnComplete(() => {
                        OnTurnFront?.Invoke();
                        action?.Invoke();
                    });
                });

            m_CardSide = CardSide.Front;
        }
    }
}
