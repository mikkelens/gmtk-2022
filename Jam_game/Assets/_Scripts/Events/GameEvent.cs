using System;
using System.Collections;
using Management;
using Tools;
using UnityEngine;

namespace Events
{
	[Serializable]
	public class GameEvent : ExpandableScriptableObject
	{
		public Optional<float> eventTime = 10f;
		public Optional<float> extraStartDelay;

		public EventsManager Manager { protected get; set; }
		protected float StartTime;
		
		protected bool GameIsPaused => GameManager.Instance.State == GameState.PausedDuringPlay;
		protected float TimeCompletion => eventTime.Enabled && eventTime.Value != 0f
			? StartTime.TimeSince() / eventTime.Value : 0f;
		protected virtual float EventCompletion => TimeCompletion;
		protected virtual bool EndEvent => EventCompletion >= 1f;

		public virtual IEnumerator RunEvent() // base as setup
		{
			if (extraStartDelay.Enabled) yield return new WaitForSeconds(extraStartDelay.Value);
			StartTime = Time.time;
			yield return Manager.StartCoroutine(PausingPoint());
		}

		protected IEnumerator PausingPoint()
		{
			if (GameIsPaused) yield return new WaitUntil(() => !GameIsPaused);
		}
	}
}