using Core.Battle;
using DG.Tweening;
using UnityEngine;

namespace Core.Battle
{
    public class CardHandView : CardView
    {
        //TODO: on hold show info
        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);    
        }

        public void Shift(float distance)
        {
            transform.DOLocalMoveX(distance, 1f);
        }
    }
}
