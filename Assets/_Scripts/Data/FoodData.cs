using UnityEngine;

namespace JustGame.Script.Data
{
    [CreateAssetMenu(menuName = "JustGame/Food Data")]
    public class FoodData : ScriptableObject
    {
        public FoodType FoodType;
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