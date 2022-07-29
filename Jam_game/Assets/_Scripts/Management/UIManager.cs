using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Management
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager Instance;
		
		[SerializeField] private List<RectTransform> pauseMenuElements = new List<RectTransform>();
		[SerializeField] private TextMeshProUGUI killScoreText;
		
		private GameManager _gameManager;
		
		private void Awake() => Instance = this;
		private void Start()
		{
			_gameManager = GameManager.Instance;
		}
		
		// activated outside
		public void UpdateKillCount(int amount)
		{
			if (killScoreText == null) return;
			killScoreText.text = amount.ToString();
		}

		public void TogglePauseMenu(bool show)
		{
			pauseMenuElements.ForEach(element => element.gameObject.SetActive(show));
		}

	}
}