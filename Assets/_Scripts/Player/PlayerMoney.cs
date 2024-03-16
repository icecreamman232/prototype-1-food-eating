using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Script.Player
{
    public class PlayerMoney : MonoBehaviour
    {
        [SerializeField] private int  m_initMoney;
        [SerializeField] private int  m_currentMoney;
        [SerializeField] private IntEvent m_buyingFoodEvent;
        [SerializeField] private IntEvent m_updateMoneyUIEvent;

        private void Start()
        {
            m_buyingFoodEvent.AddListener(OnBuyingFood);
            m_currentMoney = m_initMoney;
            m_updateMoneyUIEvent.Raise(m_currentMoney);
        }

        private void OnBuyingFood(int price)
        {
            m_currentMoney -= price;
            m_updateMoneyUIEvent.Raise(m_currentMoney);
        }

        private void OnDestroy()
        {
            m_buyingFoodEvent.RemoveListener(OnBuyingFood);
        }
    }
}

