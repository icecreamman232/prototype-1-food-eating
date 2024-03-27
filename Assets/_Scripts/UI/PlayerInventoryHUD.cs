using JustGame.Script.Data;
using JustGame.Scripts.Managers;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Script.UI
{
    
    public class PlayerInventoryHUD : Singleton<PlayerInventoryHUD>
    {
        [Header("UI Events")]
        [SerializeField] private InventoryEvent m_updateInventoryUIEvent;
        [SerializeField] private IntEvent m_selectSlotEvent;
        [Header("View Refs")]
        [SerializeField] private InventorySlotUI[] m_inventorySlots;

        private int m_lastSelectSlotIndex = 0;

        protected override void Awake()
        {
            base.Awake();
            m_updateInventoryUIEvent.AddListener(OnUpdateInventoryUI);
            m_selectSlotEvent.AddListener(OnSelectSlot);
        }

        private void OnSelectSlot(int slotIndex)
        {
            m_inventorySlots[m_lastSelectSlotIndex].OnDeselect();
            m_inventorySlots[slotIndex].OnSelect();
            m_lastSelectSlotIndex = slotIndex;
        }


        private void OnUpdateInventoryUI(ItemData item, int amount,int slotIndex)
        {
            m_inventorySlots[slotIndex].AssignItem(item, amount);
        }
        
        private void OnDestroy()
        {
            m_updateInventoryUIEvent.RemoveListener(OnUpdateInventoryUI);
            m_selectSlotEvent.RemoveListener(OnSelectSlot);
        }
    }
}
