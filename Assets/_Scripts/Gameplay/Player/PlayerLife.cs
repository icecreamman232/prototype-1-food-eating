using JustGame.Script.Data;
using JustGame.Script.Manager;
using JustGame.Scripts.Attribute;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.Serialization;

namespace JustGame.Script.Player
{
    public class PlayerLife : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField]
        private Animator m_animator;
        [Header("Life Events")]
        [SerializeField] private LifeEventSelectEvent lifeEventSelectEvent;
        [SerializeField] private ActionEvent m_createLifeEventEvent;
        [Header("Stat Event")]
        [SerializeField] private FloatEvent m_updateEnergyUIEvent;
        [SerializeField] private FloatEvent m_updateHappinessUIEvent;
        [SerializeField] private FloatEvent m_updateStressUIEvent;
        [Header("UI Events")]
        [SerializeField] private IntEvent m_updateLifeUIEvent;
        [SerializeField] private ActionEvent m_loseGameEvent;
        [SerializeField] private ActionEvent m_restartGameEvent;
        
        [Header("Stats")] 
        [SerializeField] private float m_energy;
        [SerializeField] private float m_maxEnergy;
        [SerializeField] private float m_happiness;
        [SerializeField] private float m_maxHappiness;
        [SerializeField] private float m_stress;
        [SerializeField] private float m_maxStress;
        
        [Space]
        [Header("Life")]
        [SerializeField] private int m_currentLife;
        [SerializeField] private int m_maxLife;
        
        [Header("Time")]
        [SerializeField] [ReadOnly] private float m_lifeEventDuration;
        [SerializeField] [ReadOnly] private float m_lifeEventTimer;

        public float Energy
        {
            get => m_energy;
            set => m_energy = value;
        }
        
        public float Happiness
        {
            get => m_happiness;
            set => m_happiness = value;
        }

        public float Stress
        {
            get => m_stress;
            set => m_stress = value;
        }

        private int m_triggerEatAnim = Animator.StringToHash("trigger_Eat");
        
        private void Start()
        {
            lifeEventSelectEvent.AddListener(OnApplyLifeEvent);
            m_restartGameEvent.AddListener(OnRestartGame);
            InitializeParams();
        }

        private void InitializeParams()
        {
            m_lifeEventTimer = 3;
            m_energy = 80;
            m_happiness = 80;
            m_stress = 20;
            m_currentLife = m_maxLife;
            m_updateHappinessUIEvent.Raise(MathHelpers.Remap(m_happiness,0,m_maxHappiness,0,1));
            m_updateStressUIEvent.Raise(MathHelpers.Remap(m_stress,0,m_maxStress,0,1));
        }
        
        private void OnRestartGame()
        {
            InitializeParams();
        }
        
        private void OnApplyLifeEvent(EventData lifeEvent)
        {
            m_lifeEventDuration = lifeEvent.TimeToFinish;
            
            m_animator.SetTrigger(m_triggerEatAnim);
            
            lifeEvent.ApplyEvent(this);
            
            if (m_happiness >= m_maxHappiness)
            {
                m_happiness = m_maxHappiness;
            }
            if (m_stress >= m_maxStress)
            {
                m_stress = m_maxStress;
            }

            if (m_stress >= m_maxStress
                || m_happiness <=0 
                || m_energy <=0)
            {
                m_currentLife--;
            }
            
            if (m_currentLife <= 0)
            {
                Kill();        
            }
            
            m_updateEnergyUIEvent.Raise(MathHelpers.Remap(m_energy,0,m_maxEnergy,0,1));
            m_updateHappinessUIEvent.Raise(MathHelpers.Remap(m_happiness,0,m_maxHappiness,0,1));
            m_updateStressUIEvent.Raise(MathHelpers.Remap(m_stress,0,m_maxStress,0,1));

            m_lifeEventTimer = m_lifeEventDuration;
        }


        private void Update()
        {
            
            if (m_lifeEventTimer <= 0) return;
            
            //Update current life event duration
            m_lifeEventTimer -= Time.deltaTime;
            if (m_lifeEventTimer <= 0)
            {
                //On finishing life event, we will raise the callback to create new random life events
                m_createLifeEventEvent.Raise();
            }
        }

        private void Kill()
        {
            GameManager.Instance.Pause();
            m_loseGameEvent.Raise();
        }
        
        private void OnDestroy()
        {
            lifeEventSelectEvent.RemoveListener(OnApplyLifeEvent); 
        }
    }
}

