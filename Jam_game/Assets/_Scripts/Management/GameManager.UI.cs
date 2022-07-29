using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Management
{
    public partial class GameManager
    {
        [FoldoutGroup("UI")]
        [SerializeField] private List<RectTransform> pauseMenuElements = new List<RectTransform>();
        [FoldoutGroup("UI")]
        [SerializeField] private TextMeshProUGUI killScoreText;

        private int _killCount;
        
    #region Pausing
        public void TryPausing()
        {
            if (_state != GameState.Playing) return;
            TogglePauseMenu(true);
        }
        public void TryUnpausing()
        {
            if (_state != GameState.PausedDuringPlay) return;
            TogglePauseMenu(false);
        }
        private void TogglePauseMenu(bool show)
        {
            pauseMenuElements.ForEach(element => element.gameObject.SetActive(show));
        }
    #endregion
        
    #region HUD
        public void IncreaseKillcount()
        {
            _killCount++;
            UpdateKillCount(_killCount);
        }

        private void UpdateKillCount(int killCount)
        {
            if (killScoreText == null) return;
            killScoreText.text = killCount.ToString();
        }
    #endregion
    }
}