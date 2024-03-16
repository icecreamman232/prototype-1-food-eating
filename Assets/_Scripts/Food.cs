using JustGame.Script.Data;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Script.Food
{
    public class Food : MonoBehaviour
    {
        [SerializeField] private FoodData m_foodData;
        private Vector2 m_currentPos;
        private Camera m_mainCamera;

        private void Awake()
        {
            m_mainCamera = Camera.main;
        }

        private void Update()
        {
            transform.position = m_currentPos;
        }
        
        private void OnMouseDrag()
        {
            var pos = m_mainCamera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            m_currentPos = pos;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != LayerManager.PlayerLayer)
            {
                return;
            }

            OnConsume();
        }

        public void OnConsume()
        {
            Destroy(this.gameObject);
        }
    }
}

