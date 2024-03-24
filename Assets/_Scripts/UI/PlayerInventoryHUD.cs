using System;
using JustGame.Script.Data;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Script.UI
{
    [Serializable]
    public class ItemStack
    {
        public ItemData ItemData;
        public int StackAmount;

        ItemStack()
        {
            ItemData = null;
            StackAmount = 0;
        }
    }
    public class PlayerInventoryHUD : Singleton<PlayerInventoryHUD>
    {
        [Header("UI Events")]
        [SerializeField] private PickItemEvent m_pickItemEvent;
        [Header("Data Refs")]
        [SerializeField] private ItemStack[] m_inventory;
        [Header("View Refs")]
        [SerializeField] private InventorySlotUI[] m_inventorySlots;

        private const int MAX_SLOT = 9;
        private const int MAX_STACK = 99;
        
        
        private void Start()
        {
            m_inventory = new ItemStack[MAX_SLOT];
            m_pickItemEvent.AddListener(OnPickItem);
        }

        private void OnPickItem(ItemData pickedItem)
        {
            for (int i = 0; i < m_inventorySlots.Length; i++)
            {
                if(!CheckInventorySlotAvailable(i, pickedItem)) continue;
                AddItem(pickedItem, i);
                m_inventorySlots[i].AssignItem(pickedItem);
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
