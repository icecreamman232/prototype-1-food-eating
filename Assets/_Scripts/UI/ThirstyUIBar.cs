using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Script.UI
{
    public class ThirstyUIBar : MonoBehaviour
    {
        [SerializeField] private FloatEvent m_thirstyBarUpdateEvent;
        [SerializeField] private Image m_barImg;

        private void Awake()
        {
            m_thirstyBarUpdateEvent.AddListener(OnUpdateBar);
        }
        
        private void OnUpdateBar(float value)
        {
            m_barImg.fillAmount = value;
        }

        private void OnDestroy()
        {
            m_thirstyBarUpdateEvent.RemoveListener(OnUpdateBar);
        }
    }
}