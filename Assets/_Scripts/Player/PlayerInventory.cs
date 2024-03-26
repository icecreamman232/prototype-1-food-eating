using System;
using JustGame.Script.Data;
using UnityEngine;

namespace JustGame.Script.Player
{
    [Serializable]
    public class ItemStack
    {
        public ItemData ItemData = null;
        public int StackAmount = 0;
    }
    
    public class PlayerInventory : MonoBehaviour
    {
        [Header("UI Events")]
        [SerializeField] private ItemEvent m_pickItemEvent;
        [SerializeField] private PlayerRuntimeData m_playerRuntimeData;
        [SerializeField] private InventoryEvent m_updateInventoryUIEvent;
        [SerializeField] private ItemStack[] m_inventory;

        private const int MAX_SLOT = 9;
        private const int MAX_STACK = 99;
        
        private void Start()
        {
            m_inventory = new ItemStack[MAX_SLOT];
            for (int i = 0; i < m_inventory.Length; i++)
            {
                m_inventory[i] = new ItemStack();
            }
            
            
            m_playerRuntimeData.AssignInventory(this);
            m_pickItemEvent.AddListener(OnPickItem);
        }

        private void OnPickItem(ItemData pickedItem)
        {
            for (int i = 0; i < m_inventory.Length; i++)
            {
                if(!CheckInventorySlotAvailable(i, pickedItem)) continue;
                AddItem(pickedItem, i);
                m_updateInventoryUIEvent.Raise(pickedItem,1, i);
                return;
            }
        }
        
        public bool CheckInventorySlotAvailable(ItemData itemWantToAdd)
        {
            for (int i = 0; i < m_inventory.Length; i++)
            {
                if (CheckInventorySlotAvailable(i, itemWantToAdd))
                {
                    return true;
                }
            }
            return false;
        }
        
        
        private bool CheckInventorySlotAvailable(int index, ItemData itemWantToAdd)
        {
            //Item want to add is not same type with inventory item
            if (m_inventory[index].ItemData != null && m_inventory[index].ItemData != itemWantToAdd)
            {
                return false;
            }
            
            //The slot has max stack
            if (m_inventory[index].StackAmount >= MAX_STACK)
            {
                return false;
            }
            return true;
        }
        
        /// <summary>
        /// Check whether item was in the inventory, if so we increase stack amount.
        /// Otherwise we add new
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        private void AddItem(ItemData item, int index)
        {
            if (m_inventory[index].ItemData == null)
            {
                m_inventory[index].ItemData = item;
                m_inventory[index].StackAmount = 1;
            }
            else
            {
                m_inventory[index].StackAmount +=1;
            }
        }
        
        
        private void OnDestroy()
        {
            m_pickItemEvent.RemoveListener(OnPickItem);
        }
    }
}
    

