using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Scripts.Player
{
    /// <summary>
    /// Script that handle the job complement of player and its challenge level
    /// </summary>
    public class PlayerJob : MonoBehaviour
    {
        [SerializeField] private FloatEvent m_updateUIEvent;
        [SerializeField] private float m_jobCompleteSpeed;
        [SerializeField] private float m_jobLength;

        private float m_currentJobProgress;
        private bool m_canWork;

        private void Start()
        {
            //TODO: Should let player choose when to start job
            StartJob();
        }
        

        private void StartJob()
        {
            m_currentJobProgress = 0;
            m_canWork = true;
        }
        
        private void Update()
        {
            if (!m_canWork) return;

            m_currentJobProgress += Time.deltaTime * m_jobCompleteSpeed;

            m_updateUIEvent.Raise(MathHelpers.Remap(m_currentJobProgress,0,m_jobLength,0,1));
            
            if (m_currentJobProgress >= m_jobLength)
            {
                JobComplete();
            }
        }

        private void JobComplete()
        {
            m_canWork = false;
        }
    }
}

