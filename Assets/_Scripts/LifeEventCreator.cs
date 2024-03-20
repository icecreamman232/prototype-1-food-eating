using System.Collections.Generic;
using JustGame.Script.Data;
using JustGame.Scripts.Attribute;
using JustGame.Scripts.Data;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Script.Manager
{
    public class LifeEventCreator : MonoBehaviour
    {
        [SerializeField] private RandomLifeEventEvent m_randomLifeEventEvent;
        [SerializeField] private LifeEventSelectEvent m_lifeEventSelectEvent;
        [SerializeField] private ActionEvent m_createLifeEventEvent;
        [SerializeField] private EventData[] m_eventList;
        [SerializeField] private BoolEvent m_pauseGameEvent;
        [SerializeField] [ReadOnly] private bool m_canUpdate;
        [SerializeField] [ReadOnly] private float m_timer;

        private void Start()
        {
            m_canUpdate = true;
            m_pauseGameEvent.AddListener(OnPauseGame);
            m_createLifeEventEvent.AddListener(CreateRandomEvent);
        }
        
        private void OnPauseGame(bool isPaused)
        {
            m_canUpdate = !isPaused;
        }
        
        /// <summary>
        /// Create a list of 3 random life event so player can choose one
        /// </summary>
        private void CreateRandomEvent()
        {
            List<EventData> list = new List<EventData>(); 
            for (int i = 0; i < 3; i++)
            {
                var rand = Random.Range(0, m_eventList.Length);
                list.Add(m_eventList[rand]);
            }
            m_randomLifeEventEvent.Raise(list);
        }
        
        private void OnDestroy()
        {
            m_pauseGameEvent.RemoveListener(OnPauseGame);
            m_createLifeEventEvent.RemoveListener(CreateRandomEvent);
        }
    }
}

