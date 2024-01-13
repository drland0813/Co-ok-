using System;
using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Drland.Cook
{
	public class CountDownTimerUI : MonoBehaviour, ICountDownable
	{
		[SerializeField] private TextMeshProUGUI _countDownText;

		public void UpdateTimer(float timer)
		{
			_countDownText.text = Mathf.Ceil(timer).ToString(CultureInfo.InvariantCulture);
			if (timer < float.Epsilon)
			{
				gameObject.SetActive(false);
			}
		}
	}
}
