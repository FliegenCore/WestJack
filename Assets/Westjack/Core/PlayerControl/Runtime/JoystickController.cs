using Assets;
using Common;
using Core.Controls;
using Core.UI;
using System;
using UnityEngine;

namespace Core.UnitEntities
{
    [Order(-20)]
    public class JoystickController : MonoBehaviour, IMoveProvider, IControllerEntity
    {
        public event Action<Vector2Int> OnMove;

        [Inject] private AssetLoader m_AssetLoader;
        [Inject] private CoreUIController m_CoreCanvasController;

        private readonly AssetName m_JoystickAssetName = "Joystick";
        private JoystickView m_JoystickView;
        
        private ControlMoveButton[] m_ControlButtons;
        
        public async void PreInit()
        {
            var joystickAsset = await m_AssetLoader.LoadAsync<JoystickView>(m_JoystickAssetName);
            m_JoystickView = m_AssetLoader.InstantiateSync(joystickAsset, m_CoreCanvasController.GetBottomUI().transform);

            m_CoreCanvasController.GetBottomUI().JoystickView = m_JoystickView;
            m_ControlButtons = m_JoystickView.ControlButtons;

            foreach (var button in m_ControlButtons)
            {
                button.OnClick += Move;
            }
        }

        public void Init()
        {

            
        }

        private void Move(Vector2Int direction)
        {
            OnMove?.Invoke(direction);
        }

        private void OnDestroy()
        {
            foreach (var button in m_ControlButtons)
            {
                button.OnClick -= Move;
            }
        }
    }
}
