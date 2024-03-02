using JustGame.Script.Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace JustGame.Script.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Vector2 m_direction;
        [SerializeField] private float m_moveLength;
        [SerializeField] private float m_moveSpeed;
        private MainInputAction m_inputAction;

        private void Start()
        {
            m_inputAction = new MainInputAction();
            m_inputAction.Enable();
            m_inputAction.PC.Move.performed += MovementPerform;
            m_inputAction.PC.Move.canceled += MovementCancel;
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

            transform.position += (Vector3)(m_direction * (Time.deltaTime * m_moveSpeed));
        }
    }
}

