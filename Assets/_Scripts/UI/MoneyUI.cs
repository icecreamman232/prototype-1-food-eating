using System;
using JustGame.Scripts.ScriptableEvent;
using TMPro;
using UnityEngine;

namespace JustGame.Script.UI
{
    public class MoneyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_moneyTxt;
        [SerializeField] private IntEvent m_updateMoneyUIEvent;

        private void Start()
        {
            m_updateMoneyUIEvent.AddListener(OnUpdateMoneyUI);
        }

        private void OnUpdateMoneyUI(int moneyLeft)
        {
            m_moneyTxt.text = $"${moneyLeft}";
        }

        private void OnDestroy()
        {
            m_updateMoneyUIEvent.RemoveListener(OnUpdateMoneyUI);
        }
    }
}

