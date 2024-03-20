using System;
using UnityEngine;

namespace JustGame.Scripts.Managers
{
    public enum DayWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday,
    }
    
    [Serializable]
    public class TimeData
    {
        public int Hour;
        public int Minute;

        public void Set(int hour, int minute)
        {
            
        }
        
        public void Add(int hour = 0, int minute = 0)
        {
            var addMinute = minute;
            while (addMinute > 0)
            {
                minute += addMinute;
                if (minute >= 60)
                {
                    Hour++;
                    addMinute = minute - 60;
                }
            }
            
            Hour += hour;
        }
    }
    
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private DayWeek m_dayWeek;
        [SerializeField] private TimeData m_time;

        private void OnAddTimeFromLifeEvent(TimeData data)
        {
            
        }
    } 
}


