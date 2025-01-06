using Core.PlayerExperience;
using UnityEngine;

namespace Core.World
{
    [Order(1)]
    public class CameraController : MonoBehaviour, IControllerEntity
    {
        private const int m_Speed = 5;

        [Inject] private PlayerController m_PlayerController;

        public void PreInit()
        {

        }

        public void Init()
        {

        }

        private void Update()
        {
            FollowTarget();
        }

        private void FollowTarget()
        {
            Vector3 target = new Vector3(m_PlayerController.GetPlayerTransform().position.x,
                m_PlayerController.GetPlayerTransform().position.y, -10);

            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * m_Speed);
        }
    }
}
