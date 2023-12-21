using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public class StoveCounterVisual : MonoBehaviour
	{
		[SerializeField] private StoveCounter _stoveCounter;
		[SerializeField] private GameObject _stoveOnGameObject;
		[SerializeField] private GameObject _particleGameObject;
		[SerializeField] private GameObject _emptySlotIcon;

		private void Start()
		{
			_stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
			_stoveCounter.OnHasKitchenObject += StoveCounter_OnHasKitchenObject;
		}

		private void StoveCounter_OnHasKitchenObject(object sender, StoveCounter.OnHasKitchenObjectArgs e)
		{
			_emptySlotIcon.SetActive(!e.IsHavingKitchenObject);
		}

		private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
		{
			bool showVisual = e.State == StoveCounter.State.Frying || e.State == StoveCounter.State.Fried;
			_stoveOnGameObject.SetActive(showVisual);
			_particleGameObject.SetActive(showVisual);
		}
		
	}
}