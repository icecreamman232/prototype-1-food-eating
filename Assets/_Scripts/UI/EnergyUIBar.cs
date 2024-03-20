using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Script.UI
{
    public class EnergyUIBar : MonoBehaviour
    {
        [SerializeField] private FloatEvent m_energyBarUpdateEvent;
        [SerializeField] private Image m_barImg;

        private void Awake()
        {
            m_energyBarUpdateEvent.AddListener(OnUpdateBar);
        }
        
        private void OnUpdateBar(float value)
        {
            m_barImg.fillAmount = value;
        }

        private void OnDestroy()
        {
            m_energyBarUpdateEvent.RemoveListener(OnUpdateBar);
        }
    }
}

