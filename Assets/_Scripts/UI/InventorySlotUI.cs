using JustGame.Script.Data;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Script.UI
{
    public class InventorySlotUI : MonoBehaviour
    {
        [SerializeField] private Image m_itemImage;
        private void Start()
        {
            m_itemImage.sprite = null;
        }
        
        public void AssignItem(ItemData pickedItem)
        { 
            m_itemImage.sprite = pickedItem.ItemSprite;
        }
    }
}

