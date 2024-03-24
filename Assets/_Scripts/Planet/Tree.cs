using UnityEngine;

namespace JustGame.Script.Planet
{
    public class Tree : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject m_woodPrefab;
        [SerializeField] private GameObject m_seedPrefab;
        [SerializeField] private Vector2 m_spawnWoodOffset;
        [SerializeField] private Vector2 m_spawnSeedOffset;
        
        /// <summary>
        /// How many hit before it's dead
        /// </summary>
        [Tooltip("How many hit before it's dead")]
        [SerializeField] private int m_live;
        
        public void Interact()
        {
            m_live--;
            if (m_live <= 0)
            {
                Instantiate(m_woodPrefab, transform.position + (Vector3)m_spawnWoodOffset, Quaternion.identity,transform.parent);
                Instantiate(m_seedPrefab, transform.position + (Vector3)m_spawnSeedOffset, Quaternion.identity,transform.parent);
                Destroy(this.gameObject);
            }
        }
    }
}

