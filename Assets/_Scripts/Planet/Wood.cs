using JustGame.Script.Data;
using JustGame.Script.UI;
using UnityEngine;

namespace JustGame.Script.Planet
{
    public class Wood : MonoBehaviour, IInteractable
    {
        [SerializeField] private PickItemEvent m_pickItemEvent;
        [SerializeField] private ItemData m_itemData;
        public void Interact()
        {
            if (PlayerInventoryHUD.Instance.CheckInventorySlotAvailable(m_itemData))
            {
                m_pickItemEvent.Raise(m_itemData);
                Destroy(this.gameObject);
            }
        }
    }
}

