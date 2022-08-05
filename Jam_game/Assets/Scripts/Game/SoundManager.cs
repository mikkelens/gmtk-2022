using UnityEngine;
using UnityEngine.Audio;

namespace Game
{
	public class SoundManager : MonoBehaviour
	{
		[SerializeField] private AudioListener listener;
		[SerializeField] private AudioSource soundtrackSource;
		
		public bool Muted { get; private set; }

		public void PauseMusic(bool pause)
		{
			if (pause)
				soundtrackSource.UnPause();
			else
				soundtrackSource.Pause();
		}

		public void MuteMusic(bool mute)
		{
			soundtrackSource.mute = mute;
		}

		public void MuteAll(bool mute)
		{
			Muted = mute;
			listener.enabled = !mute;
		}
	}
}