using JustGame.Script.Data;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Script.Food
{
    public class Food : MonoBehaviour
    {
        [SerializeField] private FoodData m_foodData;
        [SerializeField] private ConsumeFoodEvent m_consumeFoodEvent;
        [SerializeField] private Run m_run;
        
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
            m_run.StopRun();
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
            m_consumeFoodEvent.Raise(m_foodData);
            Destroy(this.gameObject);
        }
    }
}

