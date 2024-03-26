using System;
using UnityEngine;

namespace JustGame.Script.Data
{
    [CreateAssetMenu(menuName = "JustGame/Scriptable Event/Inventory Event")]
    public class InventoryEvent : ScriptableObject
    {
        protected Action<ItemData,int,int> m_listeners;
        
        public void AddListener(Action<ItemData,int,int> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<ItemData,int,int> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(ItemData value,int amount,int slotIndex)
        {
            m_listeners?.Invoke(value, amount, slotIndex);
        }
    }
}

