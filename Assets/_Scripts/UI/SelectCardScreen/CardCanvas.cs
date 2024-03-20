using System.Collections.Generic;
using JustGame.Script.Data;
using JustGame.Scripts.Data;
using UnityEngine;

namespace JustGame.Scripts.UI
{
    public class CardCanvas : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private RandomLifeEventEvent m_randomLifeEventEvent;
        [SerializeField] private LifeEventSelectEvent m_lifeEventSelectEvent;
        [SerializeField] private CardUIController[] m_cardList;
        
        private void Start()
        {
            Hide();

            for (int i = 0; i < m_cardList.Length; i++)
            {
                m_cardList[i].OnSelectCard += OnCardSelected;
            }
            
            m_randomLifeEventEvent.AddListener(OnReceiveLifeEvent);
        }

        private void OnReceiveLifeEvent(List<EventData> eventList)
        {
            for (int i = 0; i < m_cardList.Length; i++)
            {
                m_cardList[i].AssignData(eventList[i]);
            }
            
            Show();
        }

        private void OnCardSelected(EventData m_cardData)
        {
            //Apply event affect on player after they chose it
            m_lifeEventSelectEvent.Raise(m_cardData);
            Hide();
        }

        private void Show()
        {
            m_canvasGroup.alpha = 1;
            m_canvasGroup.interactable = true;
        }
        
        private void Hide()
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;
        }

        private void OnDestroy()
        {
            for (int i = 0; i < m_cardList.Length; i++)
            {
                m_cardList[i].OnSelectCard -= OnCardSelected;
            }
        }
    }
}

