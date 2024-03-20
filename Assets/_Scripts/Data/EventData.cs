using JustGame.Script.Player;
using UnityEngine;

namespace JustGame.Script.Data
{
    [CreateAssetMenu(menuName = "JustGame/Food Data")]
    public class EventData : ScriptableObject
    {
        public string EventName;
        public Sprite EventSprite;
        public FoodType FoodType;
        public float HungryPts;
        public float ThirstyPts;
        public int Price;

        public virtual void ApplyEvent(PlayerLife playerLife)
        {
            playerLife.Hungry += HungryPts;
            playerLife.Thirsty += ThirstyPts;
        }
        
    }
        
    public enum FoodType
    {
        VEGGIE,
        GRAIN,
        PROTEIN,
        DAIRY,
        FAT,
    }
}