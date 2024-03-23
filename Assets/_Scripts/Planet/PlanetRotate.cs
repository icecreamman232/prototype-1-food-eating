using JustGame.Script.Player;
using JustGame.Scripts.Attribute;
using UnityEngine;

namespace JustGame.Script.Planet
{
    public class PlanetRotate : MonoBehaviour
    {
        [SerializeField] private Transform m_rotatePivot;
        [SerializeField] private float m_rotateSpeed;
        [SerializeField] private PlayerMovement m_playerMovement;
        [SerializeField] [ReadOnly] private float m_currentAngle;
        
        private void Update()
        {

            if (m_playerMovement.MovingDirection == Vector2.zero) return;

            m_currentAngle += (m_playerMovement.MovingDirection.x > 0 ? 1 : -1) * m_rotateSpeed * Time.deltaTime;
            
            if (m_currentAngle >= 360)
            {
                m_currentAngle = 0;
            }

            m_rotatePivot.rotation = Quaternion.AngleAxis(m_currentAngle, Vector3.forward);
        }
    }
}

