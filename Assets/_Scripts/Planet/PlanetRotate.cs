using JustGame.Script.Player;
using JustGame.Scripts.Attribute;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Script.Planet
{
    public class PlanetRotate : MonoBehaviour
    {
        [SerializeField] private Transform m_rotatePivot;
        [SerializeField] private float m_rotateSpeed;
        [SerializeField] private PlayerMovement m_playerMovement;
        [SerializeField] [ReadOnly] private float m_currentAngle;
        [Header("Trees")] 
        [SerializeField] private GameObject m_treePrefab;
        [SerializeField] private float m_offsetToGround;

        private const int INIT_TREE_NUM = 3;
        private bool m_isInitialized;
        private CircleCollider2D m_planetCollider;

        private void Start()
        {
            m_planetCollider = m_rotatePivot.GetComponent<CircleCollider2D>();
            GenerateTrees();
            m_isInitialized = true;
        }

        private void GenerateTrees()
        {
            var startAngle = 0;
            var avgAngle = 360 / INIT_TREE_NUM;
            var randAngleOffset = Random.Range(23, 90); //Do not crazy about this number. Its just number I like xD
            for (int i = 0; i < INIT_TREE_NUM; i++)
            {
                //Cos = x
                //Sin = y
                
                startAngle = (avgAngle * i + randAngleOffset);
                
                var xValue = Mathf.Cos(startAngle * Mathf.Deg2Rad);
                var yValue = Mathf.Sin(startAngle * Mathf.Deg2Rad);
                
                //Tree was rotated 90 originally so here we subtract it
                var quaternion = Quaternion.AngleAxis(startAngle - 90, Vector3.forward);
                
                var tree = Instantiate(m_treePrefab, 
                    new Vector3(xValue, yValue) * (m_planetCollider.radius - m_offsetToGround),
                    quaternion, 
                    m_rotatePivot);
                tree.name = $"Tree-{i}";
                
            }
        }


        private void Update()
        {
            if (!m_isInitialized) return;
            
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

