using JustGame.Script.Data;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Script.Player
{
    public class PlayerConsumeFood : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private ConsumeFoodEvent m_consumeFoodEvent;
        [SerializeField] private FloatEvent m_updateHungryBarUIEvent;
        [SerializeField] private FloatEvent m_updateThirstyBarUIEvent;
        [Header("Hungry")] 
        [SerializeField] private float m_hungry;
        [SerializeField] private float m_maxHungry;
        [SerializeField] private float m_decayHungrySpeed;
        [Header("Thirsty")]
        [SerializeField] private float m_thirsty;
        [SerializeField] private float m_maxThirsty;
        [SerializeField] private float m_decayThirstySpeed;
        
        private void Start()
        {
            m_consumeFoodEvent.AddListener(OnConsumeFood);
            m_hungry = 50;
            m_thirsty = 80;
            m_updateHungryBarUIEvent.Raise(MathHelpers.Remap(m_hungry,0,m_maxHungry,0,1));
            m_updateThirstyBarUIEvent.Raise(MathHelpers.Remap(m_thirsty,0,m_maxThirsty,0,1));
        }

        private void OnConsumeFood(FoodData food)
        {
            m_hungry += food.HungryPts;
            if (m_hungry >= m_maxHungry)
            {
                m_hungry = m_maxHungry;
            }
            
            m_thirsty += food.ThirstyPts;
            if (m_thirsty >= m_maxThirsty)
            {
                m_thirsty = m_maxThirsty;
            }
            
            m_updateHungryBarUIEvent.Raise(MathHelpers.Remap(m_hungry,0,m_maxHungry,0,1));
            m_updateThirstyBarUIEvent.Raise(MathHelpers.Remap(m_thirsty,0,m_maxThirsty,0,1));
        }

        private void Update()
        {
            m_hungry -= Time.deltaTime * m_decayHungrySpeed;
            m_thirsty -= Time.deltaTime * m_decayThirstySpeed;
            if (m_hungry <= 0)
            {
                m_hungry = 0;
            }
            if (m_thirsty <= 0)
            {
                m_thirsty = 0;
            }
            
            
            m_updateHungryBarUIEvent.Raise(MathHelpers.Remap(m_hungry,0,m_maxHungry,0,1));
            m_updateThirstyBarUIEvent.Raise(MathHelpers.Remap(m_thirsty,0,m_maxThirsty,0,1));
        }


        private void OnDestroy()
        {
            m_consumeFoodEvent.RemoveListener(OnConsumeFood); 
        }
    }
}

