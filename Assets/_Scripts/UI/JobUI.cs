using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class JobUI : MonoBehaviour
    {
        [SerializeField] private FloatEvent m_jobUIUpdateEvent;
        [SerializeField] private Image m_bar;

        private void Start()
        {
            m_jobUIUpdateEvent.AddListener(OnUpdateBar);
        }


        private void OnUpdateBar(float fillValue)
        {
            m_bar.fillAmount = fillValue;
        }

        private void OnDestroy()
        {
            m_jobUIUpdateEvent.RemoveListener(OnUpdateBar);
        }
    }
}

