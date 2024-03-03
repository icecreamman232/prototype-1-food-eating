using JustGame.Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JustGame.Script.Weapon
{
    public class ProjectileWeapon : MonoBehaviour
    {
        [SerializeField] private float m_delayBetweenTwoShot;
        [SerializeField] private Vector2 m_offsetShoot;
        [SerializeField] private ObjectPooler m_objectPooler;
        [SerializeField] private InputActionAsset m_inputAction;
        private Vector2 m_shootDirection;
        private float m_timerBetweenTwoShot;
        private bool m_canShoot;
        
        private void Start()
        {
            var actionMap = m_inputAction.FindActionMap("PC");
            var action = actionMap.FindAction("Shoot");
            action.performed += Shoot;
            m_canShoot = true;
            m_timerBetweenTwoShot = 0;
        }

        private void Shoot(InputAction.CallbackContext context)
        {
            if (!m_canShoot) return;
            
            m_shootDirection = context.ReadValue<Vector2>();

            SmoothInputData();
            
            var projectileGO = m_objectPooler.GetPooledGameObject();
            var projectile = projectileGO.GetComponent<Projectile>();
            projectileGO.SetActive(true);
            projectile.SpawnProjectile(
                transform.position + (Vector3)(m_offsetShoot*m_shootDirection),
                m_shootDirection);
            m_timerBetweenTwoShot = m_delayBetweenTwoShot;
            m_canShoot = false;
        }

        /// <summary>
        /// Eliminate diagonally shooting direction
        /// </summary>
        private void SmoothInputData()
        {
            if (m_shootDirection.x != 0 && m_shootDirection.y != 0)
            {
                m_shootDirection.x = 0;
            }
        }

        private void Update()
        {
            if (m_timerBetweenTwoShot <= 0) return;
            m_timerBetweenTwoShot -= Time.deltaTime;
            if (m_timerBetweenTwoShot <= 0)
            {
                m_canShoot = true;
            }
        }
    }
}

