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
        [SerializeField] protected SpriteRenderer m_spriteRenderer;
        
        protected Vector2 m_direction;
        protected bool m_canMove;
        protected float m_traveledDistance;
        protected Vector2 m_originPos;

        private void Start()
        {
            m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }


        public virtual void SpawnProjectile(Vector2 position, Vector2 direction)
        {
            m_traveledDistance = 0;
            transform.position = position;
            m_originPos = position;
            m_direction = direction;

            var angle = Mathf.Atan2(m_direction.y, m_direction.x) * Mathf.Rad2Deg;
            
            //Rotate on game object that has the sprite only
            m_spriteRenderer.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
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

