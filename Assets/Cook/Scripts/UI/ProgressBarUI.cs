using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.Cook
{
	public class ProgressBarUI : MonoBehaviour
	{
		[SerializeField] private Image _barImage;
		[SerializeField] private GameObject _hasProgressGameObject;

		private IHasProgress _iHasProgress;

		private void Start()
		{
			_iHasProgress = _hasProgressGameObject.GetComponent<IHasProgress>();
			_iHasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
			_barImage.fillAmount = 0;
			EnableProgressBarUI(false);
		}

		private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
		{
			_barImage.fillAmount = e.ProgressNormalized;
			var enableUI = e.ProgressNormalized is > 0 and < 1;
			EnableProgressBarUI(enableUI);
		}

		private void EnableProgressBarUI(bool enable)
		{
			gameObject.SetActive(enable);
		}
	}
}
