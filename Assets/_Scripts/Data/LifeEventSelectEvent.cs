using System;
using UnityEngine;

namespace JustGame.Script.Data
{
    [CreateAssetMenu(menuName = "JustGame/Event/Consume Food")]
    public class LifeEventSelectEvent : ScriptableObject
    {
        protected Action<EventData> m_listeners;
        
        public void AddListener(Action<EventData> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<EventData> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(EventData value)
        {
            m_listeners?.Invoke(value);
        }
    }
}

