using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class CoreButton : MonoBehaviour
    {
        private event Action m_OnClick;

        [SerializeField] private Button m_Button;

        private void Awake()
        {
            m_Button.onClick.AddListener(Click);
        }

        public void Subscribe(Action action)
        {
            m_OnClick += action;
        }

        private void Unsubscribe(Action action)
        {
            m_OnClick -= action;
        }

        protected virtual void Click()
        {
            m_OnClick?.Invoke();
        }
    }
}
