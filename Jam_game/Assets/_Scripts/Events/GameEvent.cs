using System;
using System.Collections;
using Management;
using Sirenix.OdinInspector;
using Tools;
using UnityEngine;

namespace Events
{
	[Serializable]
	public class GameEvent : ExpandableScriptableObject
	{
		[PropertyOrder(5)]
		public Optional<float> eventTime = 10f;
		protected float StartTime;

		protected bool GameIsPaused => GameManager.Instance.State == GameState.PausedDuringPlay;
		protected float TimeCompletion => eventTime.Enabled && eventTime.Value != 0f
			? StartTime.TimeSince() / eventTime.Value : 0f;
		protected virtual float EventCompletion => TimeCompletion;
		protected virtual bool EndEvent => EventCompletion >= 1f;

		public virtual IEnumerator RunEvent() // base as setup
		{
			StartTime = Time.time;
			yield break;
		}

		protected virtual IEnumerator PausingPoint()
		{
			if (GameIsPaused) yield return new WaitUntil(() => !GameIsPaused);
		}
	}
}