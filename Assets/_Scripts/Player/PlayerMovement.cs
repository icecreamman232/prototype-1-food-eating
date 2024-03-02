using JustGame.Script.Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JustGame.Script.Player
{
    public enum FacingDirection
    {
        RIGHT,
        LEFT,
    }
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private float m_moveLength;
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private FacingDirection m_facingDirection;
        [SerializeField] private Vector2 m_direction;

        [Header("References")]
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        
        private MainInputAction m_inputAction;

        private void Start()
        {
            m_inputAction = new MainInputAction();
            m_inputAction.Enable();
            m_inputAction.PC.Move.performed += MovementPerform;
            m_inputAction.PC.Move.canceled += MovementCancel;

            m_facingDirection = FacingDirection.RIGHT;
        }
        
        private void MovementPerform(InputAction.CallbackContext context)
        {
            m_direction =  context.ReadValue<Vector2>();
        }
        
        private void MovementCancel(InputAction.CallbackContext context)
        {
            m_direction =  context.ReadValue<Vector2>();
        }
        
        private void OnDestroy()
        {
            m_inputAction.Disable();
        }
        
        
        private void Update()
        {
            if (m_direction == Vector2.zero) return;

            FlipPlayer();
            
            transform.position += (Vector3)(m_direction * (Time.deltaTime * m_moveSpeed));
        }

        private void FlipPlayer()
        {
            if (m_direction.x > 0 && m_facingDirection == FacingDirection.LEFT)
            {
                m_spriteRenderer.flipX = !m_spriteRenderer.flipX;
                m_facingDirection = FacingDirection.RIGHT;
            }
            else if(m_direction.x < 0 && m_facingDirection == FacingDirection.RIGHT)
            {
                m_spriteRenderer.flipX = !m_spriteRenderer.flipX;
                m_facingDirection = FacingDirection.LEFT;
            }
        }
    }
}

