using JustGame.Script.Manager;
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
        [SerializeField] private ActionEvent m_lostGameEvent;
        [SerializeField] private ActionEvent m_restartGameEvent;

        private void Start()
        {
            m_buyingFoodEvent.AddListener(OnBuyingFood);
            m_restartGameEvent.AddListener(OnRestartGame);
            m_currentMoney = m_initMoney;
            m_updateMoneyUIEvent.Raise(m_currentMoney);
        }

        private void OnRestartGame()
        {
            m_currentMoney = m_initMoney;
            m_updateMoneyUIEvent.Raise(m_currentMoney);
        }

        private void OnBuyingFood(int price)
        {
            m_currentMoney -= price;
            m_updateMoneyUIEvent.Raise(m_currentMoney);
            if (m_currentMoney <= 0)
            {
                GameManager.Instance.Pause();
                m_lostGameEvent.Raise();
            }
        }

        private void OnDestroy()
        {
            m_buyingFoodEvent.RemoveListener(OnBuyingFood);
            m_restartGameEvent.RemoveListener(OnRestartGame);
        }
    }
}

