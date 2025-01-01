using Core.UnitEntities;
using UnityEngine;

namespace Core.PlayerExperience
{
    public class Player : MonoBehaviour
    {
        private UnitMovement m_UnitMovement;

        public void Init(IMoveProvider moveProvider)
        {
            m_UnitMovement = GetComponent<UnitMovement>();
            m_UnitMovement.Init(moveProvider);
        }
    }
}
