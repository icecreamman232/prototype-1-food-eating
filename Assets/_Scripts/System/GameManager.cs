using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Script.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private BoolEvent m_pauseGameEvent;
        
        private bool m_isPaused;
        
        public void Pause()
        {
            Time.timeScale = 0;
            m_isPaused = true;
            m_pauseGameEvent.Raise(true);
        }

        public void Unpause()
        {
            Time.timeScale = 1;
            m_isPaused = false;
            m_pauseGameEvent.Raise(false);
        }
    }
}

