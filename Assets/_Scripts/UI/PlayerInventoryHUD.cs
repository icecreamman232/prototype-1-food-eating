using JustGame.Script.Data;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Script.UI
{
    
    public class PlayerInventoryHUD : Singleton<PlayerInventoryHUD>
    {
        [Header("UI Events")]
        [SerializeField] private InventoryEvent m_updateInventoryUIEvent;
        [Header("View Refs")]
        [SerializeField] private InventorySlotUI[] m_inventorySlots;
        
        private void Start()
        {
            m_updateInventoryUIEvent.AddListener(OnUpdateInventoryUI);
        }

        private void OnUpdateInventoryUI(ItemData item, int amount,int slotIndex)
        {
            m_inventorySlots[slotIndex].AssignItem(item, amount);
        }
        
        private void OnDestroy()
        {
            m_updateInventoryUIEvent.RemoveListener(OnUpdateInventoryUI);
        }
    }
}
