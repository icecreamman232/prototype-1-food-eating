using System.Collections;
using UnityEngine;

namespace JustGame.Script.Weapon
{
    public class Projectile : MonoBehaviour
    {
        [Header("Base Params")]
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private float m_maxDistance;
        [SerializeField] private float m_delayBeforeDestroy;
        [SerializeField] private LayerMask m_targetMask;
        
        protected Vector2 m_direction;
        protected bool m_canMove;
        protected float m_traveledDistance;
        protected Vector2 m_originPos;
        
        public virtual void SpawnProjectile(Vector2 position, Vector2 direction)
        {
            m_traveledDistance = 0;
            transform.position = position;
            m_originPos = position;
            m_direction = direction;
            m_canMove = true;
        }

        protected virtual void Update()
        {
            if (!isActiveAndEnabled) return;

            if (!m_canMove) return; 
            
            transform.Translate(m_direction * (Time.deltaTime* m_moveSpeed));

            m_traveledDistance = Vector2.Distance(m_originPos, transform.position);
            if (m_traveledDistance >= m_maxDistance)
            {
                DestroyProjectile();
            }
        }

        protected virtual void DestroyProjectile()
        {
            StartCoroutine(DestroyRoutine());
        }

        protected virtual IEnumerator DestroyRoutine()
        {
            if (!m_canMove)
            {
                yield break;
            }
            m_canMove = false;
            yield return new WaitForSeconds(m_delayBeforeDestroy);
            OnDisable();
        }

        protected virtual void OnDisable()
        {
            this.gameObject.SetActive(false);
        }
    }
}

