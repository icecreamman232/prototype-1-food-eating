using System;
using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

namespace JustGame.Script.UI
{
    public class LoseCanvas : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private RestartGameButton m_restartGameButton;
        [SerializeField] private ActionEvent m_OnLoseEvent;
        [SerializeField] private ActionEvent m_OnRestartEvent;

        private void Awake()
        {
            HideUI();
            m_OnLoseEvent.AddListener(OnLose);
            m_OnRestartEvent.AddListener(OnRestartGame);
        }

        private void OnRestartGame()
        {
            HideUI();
        }

        private void ShowUI()
        {
            m_canvasGroup.alpha = 1;
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
            m_restartGameButton.interactable = true;
        }

        private void HideUI()
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;
            m_restartGameButton.interactable = false;
        }
        
        private void OnLose()
        {
            ShowUI();
        }

        private void OnDestroy()
        {
            m_OnLoseEvent.RemoveListener(OnLose);
            m_OnRestartEvent.RemoveListener(OnRestartGame);
        }
    }
}


