using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cook
{
	public class ProgressBarUI : MonoBehaviour
	{
		[SerializeField] private Image _barImage;
		[SerializeField] GameObject _hasProgressGameObject;

		private IHasProgress _iHasProgress;

		private void Start()
		{
			_iHasProgress = _hasProgressGameObject.GetComponent<IHasProgress>();
			_iHasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
			_barImage.fillAmount = 0;
			EnableProgessBarUI(false);
		}

		private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
		{
			_barImage.fillAmount = e.ProgessNomarlized;
			bool enableUI = e.ProgessNomarlized > 0 && e.ProgessNomarlized < 1;
			EnableProgessBarUI(enableUI);
		}

		private void EnableProgessBarUI(bool enable)
		{
			gameObject.SetActive(enable);
		}
	}
}
