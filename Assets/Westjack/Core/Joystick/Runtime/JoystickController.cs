using Assets;
using Common;
using Core.Controls;
using Core.UI;
using Core.World;
using System;
using UnityEngine;

namespace Core.UnitEntities
{
    [Order(-20)]
    public class JoystickController : MonoBehaviour, IMoveProvider, IControllerEntity
    {
        public event Action<Vector2Int> OnMove;

        private readonly AssetName m_JoystickAssetName = "Joystick";

        [Inject] private AssetLoader m_AssetLoader;
        [Inject] private CoreUIController m_CoreCanvasController;

        private JoystickView m_JoystickView;
        
        private ControlMoveButton[] m_ControlButtons;
        private FloorController m_FloorController;

        public FloorController FloorController => m_FloorController;

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

        public void Init(FloorController floorController)
        {
            m_FloorController = floorController;
        }

        public void Move(Vector2Int direction)
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
