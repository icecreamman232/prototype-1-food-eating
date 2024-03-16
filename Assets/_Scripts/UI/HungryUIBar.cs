using System;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Script.UI
{
    public class HungryUIBar : MonoBehaviour
    {
        [SerializeField] private FloatEvent m_hungryBarUpdateEvent;
        [SerializeField] private Image m_barImg;

        private void Awake()
        {
            m_hungryBarUpdateEvent.AddListener(OnUpdateBar);
        }
        
        private void OnUpdateBar(float value)
        {
            m_barImg.fillAmount = value;
        }

        private void OnDestroy()
        {
            m_hungryBarUpdateEvent.RemoveListener(OnUpdateBar);
        }
    }
}
