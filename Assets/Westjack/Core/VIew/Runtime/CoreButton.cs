using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.View
{
    public class CoreButton : MonoBehaviour
    {
        private event Action m_OnClick;

        [SerializeField] private Button m_Button;

        private void Awake()
        {
            m_Button.onClick.AddListener(OnClick);
        }

        public void Subscribe(Action action)
        {
            m_OnClick += action;
        }

        private void Unsubscribe(Action action)
        {
            m_OnClick -= action;
        }

        private void OnClick()
        {
            m_OnClick?.Invoke();
        }
    }
}
