using System;
using JustGame.Script.Data;
using JustGame.Scripts.Attribute;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Scripts.UI
{
    public class CardUIController : Selectable
    {
        [SerializeField][ReadOnly]private EventData m_eventData;
        [SerializeField] private Image m_cardImage;
        [SerializeField] private TextMeshProUGUI m_placeholderName;
        [SerializeField] private TextMeshProUGUI m_energyValueTxt;
        [SerializeField] private TextMeshProUGUI m_happinessValueTxt;
        [SerializeField] private TextMeshProUGUI m_stressValueTxt;
        public Action<EventData> OnSelectCard;

        public void AssignData(EventData data)
        {
            m_eventData = data;
            m_cardImage.sprite = data.EventSprite;
            m_placeholderName.text = data.EventName;
            m_energyValueTxt.text = data.EnergyPts.ToString();
            m_happinessValueTxt.text = data.HappinessPts.ToString();
            m_stressValueTxt.text = data.StressPts.ToString();
        }
        

        public override void OnPointerDown(PointerEventData eventData)
        {
            OnSelectCard.Invoke(m_eventData);
            base.OnPointerDown(eventData);
        }
    }
}