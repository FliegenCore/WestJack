using TMPro;
using UnityEngine;

namespace Core.Battle
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_RankText;

        public CardView SetType(CardType type)
        {

            return this;
        }

        public CardView SetRank(int rank)
        {
            m_RankText.text = rank.ToString();

            return this;
        }
    }
}
