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
        [SerializeField] private EventData[] m_eventList;
        [SerializeField] private float m_minDelaySpawn;
        [SerializeField] private float m_maxDelaySpawn;
        [SerializeField] private BoolEvent m_pauseGameEvent;
        [SerializeField] [ReadOnly] private bool m_canUpdate;
        [SerializeField] [ReadOnly] private float m_timer;

        private void Start()
        {
            m_canUpdate = true;
            m_timer = GetSpawnTime();
            m_pauseGameEvent.AddListener(OnPauseGame);
            m_lifeEventSelectEvent.AddListener(OnSelectLifeEvent);
        }

        private void OnSelectLifeEvent(EventData lifeEvent)
        {
            if (lifeEvent != null)
            {
                m_timer = GetSpawnTime();
            }
        }


        private void OnPauseGame(bool isPaused)
        {
            m_canUpdate = !isPaused;
        }

        private void Update()
        {
            if (!m_canUpdate) return;
            
            if (m_timer <= 0) return;

            m_timer -= Time.deltaTime;
            if (m_timer <= 0)
            {
                CreateRandomEvent();
            }
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
        
        private float GetSpawnTime()
        {
            return Random.Range(m_minDelaySpawn, m_maxDelaySpawn);
        }

        private void OnDestroy()
        {
            m_pauseGameEvent.RemoveListener(OnPauseGame);
            m_lifeEventSelectEvent.RemoveListener(OnSelectLifeEvent);
        }
    }
}

