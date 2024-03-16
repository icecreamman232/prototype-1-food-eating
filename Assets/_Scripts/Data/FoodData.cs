using UnityEngine;

namespace JustGame.Script.Data
{
    [CreateAssetMenu(menuName = "JustGame/Food Data")]
    public class FoodData : ScriptableObject
    {
        public FoodType FoodType;
        public float HungryPts;
        public float ThirstyPts;
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