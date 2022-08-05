using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game
{
	public class UIManager : MonoBehaviour
	{
		public static UIManager Instance;
		
		[FoldoutGroup("Cursor")]
		[ShowIf("@defaultCursorSprite == null")]
		[SerializeField] private Texture2D defaultCursorTexture;
		[FoldoutGroup("Cursor")]
        [ShowIf("@defaultCursorTexture == null")]
        [SerializeField] private Sprite defaultCursorSprite;

		[SerializeField] private List<RectTransform> pauseMenuElements = new List<RectTransform>();
		[SerializeField] private TextMeshProUGUI killScoreText;

		private Texture2D _cursorTexture;
		public Texture2D CursorTexture
		{
			get
			{
				if (_cursorTexture != null) return _cursorTexture;
				if (defaultCursorTexture != null) return defaultCursorTexture;
				if (defaultCursorSprite != null) return defaultCursorSprite.texture;
				return null;
			}
			set => _cursorTexture = value;
		}
		
		private void Awake() => Instance = this;
		
		private void OnApplicationFocus(bool hasFocus)
		{
			UseCustomCursor(hasFocus);
		}
		private void UseCustomCursor(bool custom)
		{
			Texture2D cursor = null;
			if (custom) cursor = CursorTexture;
			Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
		}
		
		// activated outside //
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