using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	public class StoveCounterSound : MonoBehaviour
	{
		[SerializeField] private StoveCounter _stoveCounter;
		[SerializeField] private AudioSource _audioSource;

		private void Start()
		{
			_stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
		}

		private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
		{
			bool playSound = e.State == StoveCounter.State.Frying || e.State == StoveCounter.State.Fried;
			if (playSound)
			{
				_audioSource.Play();
			}
			else
			{
				_audioSource.Pause();
			}
		}
	}
}