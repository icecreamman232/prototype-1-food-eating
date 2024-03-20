using System;
using System.Collections.Generic;
using JustGame.Script.Data;
using UnityEngine;

namespace JustGame.Scripts.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Random Life Event")]
    public class RandomLifeEventEvent : ScriptableObject
    {
        protected Action<List<EventData>> m_listeners;
        
        public void AddListener(Action<List<EventData>> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<List<EventData>> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(List<EventData> value)
        {
            m_listeners?.Invoke(value);
        }
    }
}

