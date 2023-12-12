using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	public class SelectedCounter : MonoBehaviour
	{
		[SerializeField] private BaseCounter _clearCounter;
		[SerializeField] private GameObject[] _visualObjects;

		private void Start()
		{
			PlayerController.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
		}

		private void Player_OnSelectedCounterChanged(object sender, PlayerController.OnSelectedCounterChangedArgs e)
		{
			if (e.SelectedCounter == _clearCounter)
			{
				Show();
			}
			else
			{
				Hide();
			}
		}

		private void Show()
		{
			foreach (GameObject item in _visualObjects)
			{
				item.SetActive(true);
			}
		}

		private void Hide()
		{
			foreach (GameObject item in _visualObjects)
			{
				item.SetActive(false);
			}
		}
	}

}