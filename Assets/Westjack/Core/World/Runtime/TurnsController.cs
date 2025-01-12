using Core.Common;
using Core.PlayerExperience;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.UnitEntities
{
    [Order(999)]
    public class TurnsController : MonoBehaviour, IControllerEntity
    {
        [Inject] private PlayerController m_PlayerController;
        [Inject] private EventManager m_EventManager;

        private List<ITurned> m_Turndeds;

        public void PreInit()
        {
            m_Turndeds = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ITurned>().ToList();
        }

        public void Init()
        {
            m_EventManager.Subscribe<SubscribeOnDoTurnSignal, ITurned>(this, SubscribeOnTurn);
            m_PlayerController.SubscribeOnPlayerMove(DoTurns);
        }

        private void DoTurns()
        {
            foreach (var turn in m_Turndeds)
            {
                turn.DoTurn();
            }
        }

        private void SubscribeOnTurn(ITurned tunred)
        {
            if (m_Turndeds.Contains(tunred))
            {
                return;
            }

            m_Turndeds.Add(tunred);
        }

        private void UnsubscribeOnTurn(ITurned tunred)
        {
            if (!m_Turndeds.Contains(tunred))
            {
                return;
            }

            m_Turndeds.Remove(tunred);
        }

        private void OnDestroy()
        {
            m_PlayerController.UnsubscribeOnPlayerMove(DoTurns);

            m_EventManager.Unsubscribe<SubscribeOnDoTurnSignal>(this);
        }
    }
}
