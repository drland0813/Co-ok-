using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public class SinkCounterSound : MonoBehaviour
	{
		[SerializeField] private SinkCounter _sinkCounter;
		[SerializeField] private AudioSource _audioSource;

		private void Start()
		{
			_sinkCounter.OnWashing += PlaySoundWashing;
		}

		private void PlaySoundWashing(bool isWashing)
		{
			if (isWashing)
			{
				if (_audioSource.isPlaying) return;
				_audioSource.Play();
			}
			else
			{
				if (!_audioSource.isPlaying) return;
				_audioSource.Pause();
			}
		}
	}
}