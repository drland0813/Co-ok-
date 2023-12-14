using System;
using TMPro;
using UnityEngine;

namespace Drland.Cook
{
	public class CountDownTimerUI : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _countDownText;

		private void Start()
		{
			GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
		}

		private void Update()
		{
			_countDownText.text = Mathf.Ceil(GameManager.Instance.GetCountDownTimer()).ToString();
		}

		private void GameManager_OnStateChanged(object sender, EventArgs e)
		{
			var isCountDownToStartActive = GameManager.Instance.IsCountDownToStartActive();
			if (isCountDownToStartActive) Show();
			else Hide();
		}

		private void Show()
		{
			gameObject.SetActive(true);
		}

		private void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}
