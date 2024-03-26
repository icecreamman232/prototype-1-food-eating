using JustGame.Script.Player;
using UnityEngine;

namespace JustGame.Script.Data
{
    [CreateAssetMenu(menuName = "JustGame/Runtime/Player")]
    public class PlayerRuntimeData : ScriptableObject
    {
        [SerializeField] private PlayerInventory m_inventory;

        public PlayerInventory Inventory => m_inventory;
        
        public void AssignInventory(PlayerInventory inventory)
        {
            m_inventory = inventory;
        }

        private void OnDisable()
        {
            m_inventory = null;
        }
    }
}

