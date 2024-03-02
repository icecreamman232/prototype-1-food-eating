using UnityEngine;

namespace JustGame.Script.Data
{
    [CreateAssetMenu(menuName = "JustGame/LayerData",fileName = "LayerData")]
    public class LayerScriptableData : ScriptableObject
    {
        [SerializeField] private LayerMask m_obstacle;

        public LayerMask ObstacleMask => m_obstacle;
    }
}

