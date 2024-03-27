using System;
using JustGame.Script.Data;
using JustGame.Scripts.ScriptableEvent;
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
        [SerializeField] private ItemData m_playerGauntlet;
        [Header("UI Events")]
        [SerializeField] private ItemEvent m_pickItemEvent;
        [SerializeField] private IntEvent m_selectSlotEvent;
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

            //Add gauntlet as default item
            m_inventory[0].ItemData = m_playerGauntlet;
            m_inventory[0].StackAmount = 1;
            m_updateInventoryUIEvent.Raise(m_playerGauntlet,1, 0);
            
            m_playerRuntimeData.AssignInventory(this);
            m_pickItemEvent.AddListener(OnPickItem);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                m_selectSlotEvent.Raise(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                m_selectSlotEvent.Raise(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                m_selectSlotEvent.Raise(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                m_selectSlotEvent.Raise(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                m_selectSlotEvent.Raise(4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                m_selectSlotEvent.Raise(5);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                m_selectSlotEvent.Raise(6);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                m_selectSlotEvent.Raise(7);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                m_selectSlotEvent.Raise(8);
            }
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
    

