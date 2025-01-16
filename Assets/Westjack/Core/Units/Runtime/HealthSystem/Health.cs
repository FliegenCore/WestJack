using System;
using R3;
using UnityEngine;

namespace Core.UnitEntities
{
    public class Health : MonoBehaviour
    {
        private HealthData m_HealthData;

        public void Init()
        {
            m_HealthData = new HealthData();
            m_HealthData.Init(3);
        }

        public void TakeDamage(int damage)
        {
            m_HealthData.TakeDamage(damage);
        }

        public void RestoreHealth(int health)
        {
            m_HealthData.RestoreHeath(health);
        }

        public void SubscribeOnHealthChanged(Action<int> callback)
        {
            m_HealthData.OnHealthChanged += callback; 
        }

        public void SubscribeOnTakeDamage(Action callback)
        { 
            m_HealthData.OnTakeDamage += callback;
        }

        public void SubscribeOnHealthRestiore(Action callback)
        {
            m_HealthData.OnRestoreHeath += callback;
        }

        public void UnsubscribeOnHealthChanged(Action<int> callback)
        {
            m_HealthData.OnHealthChanged -= callback;
        }

        public void UnsubscribeOnTakeDamage(Action callback)
        {
            m_HealthData.OnTakeDamage -= callback;
        }

        public void UnsubscribeOnHealthRestiore(Action callback)
        {
            m_HealthData.OnRestoreHeath -= callback;
        }
    }
}
