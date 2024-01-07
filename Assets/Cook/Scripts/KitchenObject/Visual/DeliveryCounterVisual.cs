using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.Cook
{
	[Serializable]
	public class DeliveryResultData
	{
		public Sprite Sprite;
		public Color Color;
	}
	public class DeliveryCounterVisual : MonoBehaviour
	{
		[SerializeField] private DeliveryCounter _deliveryCounter;
		[SerializeField] private Image _noti;
		[SerializeField] private DeliveryResultData _successData;
		[SerializeField] private DeliveryResultData _failData;

		private void Start()
		{
			_deliveryCounter.OnDeliveryResult += UpdateDeliveryResultUI;
		}

		private void ShowDeliveryResultUI(bool deliveryResult)
		{
			var resultData = deliveryResult ? _successData : _failData;
			_noti.sprite = resultData.Sprite;
			_noti.color = resultData.Color;
			_noti.transform.parent.gameObject.SetActive(true);
		}

		private void HideDeliveryResultUI()
		{
			_noti.transform.parent.gameObject.SetActive(false);
		}

		private void UpdateDeliveryResultUI(bool deliveryResult)
		{
			StartCoroutine(ShowResultUI(deliveryResult));
		}

		private IEnumerator ShowResultUI(bool deliveryResult)
		{
			ShowDeliveryResultUI(deliveryResult);
			yield return new WaitForSeconds(1f);
			HideDeliveryResultUI();
		}
	}
}