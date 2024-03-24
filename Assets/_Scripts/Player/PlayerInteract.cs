using JustGame.Script.Data;
using JustGame.Script.Planet;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Script.Player
{
    public class PlayerInteract : MonoBehaviour
    {
        private IInteractable m_currentInteractable;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != LayerManager.InteractableLayer) return;

            var interactable = other.GetComponent<IInteractable>();

            if (interactable != null)
            {
                m_currentInteractable = interactable;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (m_currentInteractable != null)
                {
                    m_currentInteractable.Interact();
                }
            }
        }
    }
}
