using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public class StoveCounterSound : MonoBehaviour
	{
		[SerializeField] private StoveCounter _stoveCounter;
		[SerializeField] private AudioSource _audioSource;

		private void Start()
		{
			_stoveCounter.OnStateChanged += OnStateChanged;
		}

		private void OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
		{
			var playSound = e.State is StoveCounter.State.Frying or StoveCounter.State.Fried;
			if (playSound) _audioSource.Play();
			else _audioSource.Pause();
		}
	}
}