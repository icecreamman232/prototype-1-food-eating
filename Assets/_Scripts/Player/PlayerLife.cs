using JustGame.Script.Data;
using JustGame.Script.Manager;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Script.Player
{
    public class PlayerLife : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField]
        private Animator m_animator;
        [Header("Events")]
        [SerializeField] private LifeEventSelectEvent lifeEventSelectEvent;
        [SerializeField] private FloatEvent m_updateHungryBarUIEvent;
        [SerializeField] private FloatEvent m_updateThirstyBarUIEvent;
        [SerializeField] private IntEvent m_updateLifeUIEvent;
        [SerializeField] private ActionEvent m_loseGameEvent;
        [SerializeField] private ActionEvent m_restartGameEvent;
        [Header("Hungry")] 
        [SerializeField] private float m_hungry;
        [SerializeField] private float m_maxHungry;
        [Header("Thirsty")]
        [SerializeField] private float m_thirsty;
        [SerializeField] private float m_maxThirsty;
        [Header("Life")]
        [SerializeField] private int m_currentLife;
        [SerializeField] private int m_maxLife;
        
        public float Hungry  { get; set; }
        public float Thirsty { get; set; }

        private int m_triggerEatAnim = Animator.StringToHash("trigger_Eat");
        
        private void Start()
        {
            lifeEventSelectEvent.AddListener(OnConsumeFood);
            m_restartGameEvent.AddListener(OnRestartGame);
            InitializeParams();
        }

        private void InitializeParams()
        {
            m_hungry = 50;
            m_thirsty = 80;
            m_currentLife = m_maxLife;
            m_updateHungryBarUIEvent.Raise(MathHelpers.Remap(m_hungry,0,m_maxHungry,0,1));
            m_updateThirstyBarUIEvent.Raise(MathHelpers.Remap(m_thirsty,0,m_maxThirsty,0,1));
        }
        
        private void OnRestartGame()
        {
            InitializeParams();
        }

        private void OnConsumeFood(EventData food)
        {
            m_animator.SetTrigger(m_triggerEatAnim);
            
            food.ApplyEvent(this);
            
            if (m_hungry >= m_maxHungry)
            {
                m_hungry = m_maxHungry;
            }
            if (m_thirsty >= m_maxThirsty)
            {
                m_thirsty = m_maxThirsty;
            }

            if (m_thirsty <= 0 || m_hungry <= 0)
            {
                m_currentLife--;
            }
            
            if (m_currentLife <= 0)
            {
                Kill();        
            }
            
            m_updateHungryBarUIEvent.Raise(MathHelpers.Remap(m_hungry,0,m_maxHungry,0,1));
            m_updateThirstyBarUIEvent.Raise(MathHelpers.Remap(m_thirsty,0,m_maxThirsty,0,1));
        }
        
        private void Kill()
        {
            GameManager.Instance.Pause();
            m_loseGameEvent.Raise();
        }
        
        private void OnDestroy()
        {
            lifeEventSelectEvent.RemoveListener(OnConsumeFood); 
        }
    }
}

