using JustGame.Script.Data;
using UnityEngine;

namespace JustGame.Script.Planet
{
    public class Wood : MonoBehaviour, IInteractable
    {
        [SerializeField] private PlayerRuntimeData m_playerRuntimeData;
        [SerializeField] private ItemEvent m_pickItemEvent;
        [SerializeField] private ItemData m_itemData;
        public void Interact()
        {
            if (m_playerRuntimeData.Inventory.CheckInventorySlotAvailable(m_itemData))
            {
                m_pickItemEvent.Raise(m_itemData);
                Destroy(this.gameObject);
            }
        }
    }
}

