using System;
using UnityEngine;

namespace JustGame.Script.Data
{
    [CreateAssetMenu(menuName = "JustGame/Event/Consume Food")]
    public class ConsumeFoodEvent : ScriptableObject
    {
        protected Action<FoodData> m_listeners;
        
        public void AddListener(Action<FoodData> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<FoodData> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(FoodData value)
        {
            m_listeners?.Invoke(value);
        }
    }
}

