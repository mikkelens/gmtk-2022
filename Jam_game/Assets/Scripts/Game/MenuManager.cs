using System;
using UnityEngine;

namespace Game
{
	public enum MenuState
	{
		MainMenu,
		EndMenu,
	}
	
	// this must never die or the game breaks
	public class MenuManager : MonoBehaviour
	{
		private GameManager _manager;
		
		private void Awake()
		{
			DontDestroyOnLoad(this);
		}

		private void Start()
		{
			_manager = GameManager.Instance;
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			if (pauseStatus)
			{
				_manager.State = GameState.PausedDuringPlay;
			}
		}
	}
}