using JustGame.Script.Player;
using UnityEngine;

namespace JustGame.Script.Data
{
    [CreateAssetMenu(menuName = "JustGame/Life Event Data")]
    public class EventData : ScriptableObject
    {
        public string EventName;
        public Sprite EventSprite;
        public float TimeToFinish; //Seconds
        [Header("Stats")]
        public float HappinessPts;
        public float EnergyPts;
        public float StressPts;
        
        public int Price;

        public virtual void ApplyEvent(PlayerLife playerLife)
        {
            playerLife.Happiness += HappinessPts;
            playerLife.Stress += StressPts;
        }
    }
}