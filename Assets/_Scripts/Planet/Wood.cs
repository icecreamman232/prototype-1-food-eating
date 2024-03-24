using JustGame.Script.Data;
using UnityEngine;

namespace JustGame.Script.Planet
{
    public class Wood : MonoBehaviour, IInteractable
    {
        [SerializeField] private PickItemEvent m_pickItemEvent;
        [SerializeField] private ItemData m_itemData;
        public void Interact()
        {
            m_pickItemEvent.Raise(m_itemData);
            Destroy(this.gameObject);
        }
    }
}

