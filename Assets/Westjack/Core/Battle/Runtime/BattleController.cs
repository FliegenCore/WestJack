using Assets;
using Core.Common;
using UnityEngine;

namespace Core.Battle
{
    [Order(5)]
    public class BattleController : MonoBehaviour, IControllerEntity
    {
        [Inject] private AssetLoader m_AssetLoader;
        [Inject] private EventManager m_EventManager;


        public void PreInit()
        {
        }

        public void Init()
        {
        }

        private void StartBattle()
        { 
            
        }

        private void OnDestroy()
        {
            
        }
    }
}
