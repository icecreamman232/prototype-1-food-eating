using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Script.UI
{
    public class LifeUI : MonoBehaviour
    {
        [SerializeField] private Image[] m_heartList;
        [SerializeField] private IntEvent m_updateLifeUIEvent;

        private void Start()
        {
            m_updateLifeUIEvent.AddListener(OnUpdateLifeUI);
        }


        private void OnUpdateLifeUI(int numberLifeLeft)
        {
            if (numberLifeLeft == 2)
            {
                m_heartList[2].gameObject.SetActive(false);
            }
            else if (numberLifeLeft == 1)
            {
                m_heartList[2].gameObject.SetActive(false);
                m_heartList[1].gameObject.SetActive(false);
            }
            else
            {
                m_heartList[2].gameObject.SetActive(false);
                m_heartList[1].gameObject.SetActive(false);
                m_heartList[0].gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            m_updateLifeUIEvent.RemoveListener(OnUpdateLifeUI);
        }
    }
}


