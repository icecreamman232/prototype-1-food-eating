using UnityEngine;

namespace JustGame.Script.Food
{
    public class Run : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed;
        
        private Vector2 m_from;
        private Vector2 m_to;
        private float m_lerpValue;
        
        public void SetRun(Vector2 from, Vector2 to)
        {
            m_from = from;
            m_to = to;
        }

        private void Update()
        {
            if (!this.isActiveAndEnabled) return;

            if (m_from == m_to) return;
            
            m_lerpValue += m_moveSpeed;
            transform.position = Vector2.Lerp(m_from, m_to, m_lerpValue);
            if (m_lerpValue >= 1)
            {
               Destroy(this.gameObject);
            }
        }
    }
}

