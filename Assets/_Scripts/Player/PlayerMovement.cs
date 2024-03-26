using JustGame.Scripts.Attribute;
using UnityEngine;

namespace JustGame.Script.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        [SerializeField] private float m_moveSpeed;
        [SerializeField] [ReadOnly] private Vector2 m_movingDirection;

        public Vector2 MovingDirection => m_movingDirection;
        
        private int m_idleAnim = Animator.StringToHash("trigger_Idle");
        private int m_runAnim = Animator.StringToHash("trigger_Run");
        
        private void Update()
        {
            HandleInput();
            Movement();
            UpdateAnimator();
        }

        private void HandleInput()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                m_movingDirection.y = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                m_movingDirection.y = -1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                m_movingDirection.x = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                m_movingDirection.x = 1;
            }
            else
            {
                m_movingDirection = Vector2.zero;
            }
        }

        private void Movement()
        {
            if (m_movingDirection == Vector2.zero) return;
            
            //transform.Translate(m_movingDirection * (Time.deltaTime * m_moveSpeed));
        }

        private void UpdateAnimator()
        {
            if (m_movingDirection == Vector2.zero)
            {
                m_spriteRenderer.flipX = false;
                m_animator.SetTrigger(m_idleAnim);
            }
            else
            {
                if (m_movingDirection.x <= 0)
                {
                    m_spriteRenderer.flipX = false;
                }
                else
                {
                    m_spriteRenderer.flipX = true;
                }
                m_animator.SetTrigger(m_runAnim);
            }
        }
    }
}
