using System;
using JustGame.Script.Food;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Script.Manager
{
    public class FoodChain : MonoBehaviour
    {
        [SerializeField] private GameObject[] m_foodList;
        [SerializeField] private Transform m_startPos;
        [SerializeField] private Transform m_endPos;
        [SerializeField] private float m_minDelaySpawn;
        [SerializeField] private float m_maxDelaySpawn;
        [SerializeField] private BoolEvent m_pauseGameEvent;

        private bool m_canUpdate;
        private float m_timer;

        private void Start()
        {
            m_canUpdate = true;
            m_timer = GetSpawnTime();
            m_pauseGameEvent.AddListener(OnPauseGame);
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
                SpawnFood();
                m_timer = GetSpawnTime();
            }
        }

        private void SpawnFood()
        {
            var food = Instantiate(GetFood(), m_startPos.position, Quaternion.identity);
            var foodRun = food.GetComponent<Run>();
            foodRun.SetRun(m_startPos.position, m_endPos.position);
        }

        private GameObject GetFood()
        {
            var rand = Random.Range(0, m_foodList.Length);
            return m_foodList[rand];
        }
        
        private float GetSpawnTime()
        {
            return Random.Range(m_minDelaySpawn, m_maxDelaySpawn);
        }

        private void OnDestroy()
        {
            m_pauseGameEvent.RemoveListener(OnPauseGame);
        }
    }
}

