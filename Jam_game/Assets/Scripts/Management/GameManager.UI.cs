using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Management
{
    public partial class GameManager
    {
        [FoldoutGroup("UI")]
        [SerializeField] private TextMeshProUGUI killScoreText;
        [FoldoutGroup("UI")]
        [SerializeField] private List<RectTransform> pauseMenuElements = new List<RectTransform>();
        
        public void TogglePauseMenu(bool show)
        {
            pauseMenuElements.ForEach(element => element.gameObject.SetActive(show));
        }

        public void IncreaseKillcount()
        {
            _killCount++;
            UpdateKillCount(_killCount);
        }
        
        public void UpdateKillCount(int killCount)
        {
            if (killScoreText == null) return;
            killScoreText.text = killCount.ToString();
        }
    }
}