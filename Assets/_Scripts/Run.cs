using System;
using JustGame.Script.Manager;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Script.Food
{
    public class Run : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private BoolEvent m_pauseGameEvent;
        private bool m_canRun;
        private Vector2 m_from;
        private Vector2 m_to;
        private float m_lerpValue;

        private void Start()
        {
            m_pauseGameEvent.AddListener(OnPauseGame);
        }

        private void OnPauseGame(bool isPaused)
        {
            m_canRun = isPaused;
        }

        public void SetRun(Vector2 from, Vector2 to)
        {
            m_from = from;
            m_to = to;
            m_canRun = true;
        }

        public void StopRun()
        {
            m_canRun = false;
        }

        private void Update()
        {
            if (!this.isActiveAndEnabled) return;

            if (!m_canRun) return;
            
            if (m_from == m_to) return;
            
            m_lerpValue += m_moveSpeed/10000;
            transform.position = Vector2.Lerp(m_from, m_to, m_lerpValue);
            if (m_lerpValue >= 1)
            {
               Destroy(this.gameObject);
            }
        }

        private void OnDestroy()
        {
            m_pauseGameEvent.RemoveListener(OnPauseGame);
        }
    }
}

