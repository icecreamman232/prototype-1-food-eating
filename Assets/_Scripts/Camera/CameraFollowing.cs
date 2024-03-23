using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public class CameraFollowing : MonoBehaviour
    {
        [SerializeField] private Transform m_cameraTransform;
        [SerializeField] private Transform m_targetTransform;
        [SerializeField] private float m_followingSpeed;
        [SerializeField] private Vector2 m_targetOffset;
        private Vector3 m_targetPos;

        private bool m_canFollow;
        
        private void Awake()
        {
            var camera = FindObjectOfType<UnityEngine.Camera>();
            if (camera == null)
            {
                Debug.LogError("Camera not found!");
            }
            m_cameraTransform = camera.transform;
            m_canFollow = true;
        }

        public void SetPermission(bool value)
        {
            m_canFollow = value;
        }
        public void SetTarget(Transform target)
        {
            m_targetTransform = target;
        }
        private void Update()
        {
            if (!m_canFollow) return;
            if (m_targetTransform == null) return;
            m_targetPos = m_targetTransform.position + (Vector3)m_targetOffset;
            m_targetPos.z = -10;
            m_cameraTransform.position = Vector3.Lerp(m_cameraTransform.position, m_targetPos, Time.deltaTime * m_followingSpeed);
        }

        public void ResetCamera()
        {
            m_cameraTransform.position = new Vector3(0, 0, -10);
        }
    }
}

