using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Management
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        private void Awake() => Instance = this;
        
        [SerializeField] private TextMeshProUGUI killScoreText;
        [SerializeField] private List<RectTransform> pauseMenuElements = new List<RectTransform>();
        
        public void TogglePauseMenu(bool show)
        {
            pauseMenuElements.ForEach(element => element.gameObject.SetActive(show));
        }

        public void UpdateKillCount(int killCount)
        {
            if (killScoreText == null) return;
            killScoreText.text = killCount.ToString();
        }
    }
}