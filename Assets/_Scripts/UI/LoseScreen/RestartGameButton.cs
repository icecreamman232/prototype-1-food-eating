using JustGame.Script.Manager;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Script.UI
{
    public class RestartGameButton : Selectable
    {
        [SerializeField] private ActionEvent m_restartGameEvent;
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            GameManager.Instance.Unpause();
            m_restartGameEvent.Raise();
            base.OnSelect(eventData);
            base.OnPointerDown(eventData);
        }
    }
}

