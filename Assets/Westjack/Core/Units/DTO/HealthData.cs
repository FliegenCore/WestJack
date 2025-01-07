using System;
using R3;
using UnityEngine;

namespace Core.UnitEntities
{
    public class HealthData
    {
        public event Action<int> OnHealthChanged;
        public event Action OnTakeDamage;
        public event Action OnRestoreHeath;

        private int m_CurrentHealth;
        private int m_MaxHealth;

        public void Init(int maxHealth)
        {
            m_MaxHealth = maxHealth;
            m_CurrentHealth = m_MaxHealth;

            OnHealthChanged?.Invoke(m_CurrentHealth);
        }

        public void TakeDamage(int value)
        {
            m_CurrentHealth -= value;
            OnHealthChanged?.Invoke(m_CurrentHealth);
            OnTakeDamage?.Invoke();
        }

        public void RestoreHeath(int value)
        {
            m_CurrentHealth += value;

            if (m_CurrentHealth >= m_MaxHealth)
            {
                m_CurrentHealth = m_MaxHealth;
            }

            OnHealthChanged?.Invoke(m_CurrentHealth);
            OnRestoreHeath?.Invoke();
        }
    }
}
