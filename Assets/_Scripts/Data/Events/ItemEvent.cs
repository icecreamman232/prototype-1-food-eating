using System;
using UnityEngine;

namespace JustGame.Script.Data
{
    [CreateAssetMenu(menuName = "JustGame/Scriptable Event/Item Event")]
    public class ItemEvent : ScriptableObject
    {
        protected Action<ItemData> m_listeners;
        
        public void AddListener(Action<ItemData> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<ItemData> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(ItemData value)
        {
            m_listeners?.Invoke(value);
        }
    }
}

