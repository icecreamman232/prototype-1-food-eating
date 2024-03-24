using UnityEngine;

namespace JustGame.Script.Data
{
    [CreateAssetMenu(menuName = "JustGame/Data/Item")]
    public class ItemData : ScriptableObject
    {
        public string ItemName;
        public Sprite ItemSprite;
    }
}

