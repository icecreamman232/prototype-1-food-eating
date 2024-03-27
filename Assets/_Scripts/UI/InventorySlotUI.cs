using System;
using JustGame.Script.Data;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Script.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image m_itemImage;
        [SerializeField] private Image m_outlineImg;
        private void Awake()
        {
            m_itemImage.enabled = false;
            m_outlineImg.gameObject.SetActive(false);
        }

        public void OnSelect()
        {
            m_outlineImg.gameObject.SetActive(true);
        }

        public void OnDeselect()
        {
            m_outlineImg.gameObject.SetActive(false);
        }
        
        public void AssignItem(ItemData pickedItem,int amount)
        { 
            m_itemImage.enabled = true;
            m_itemImage.sprite = pickedItem.ItemSprite;
        }
    }
}

