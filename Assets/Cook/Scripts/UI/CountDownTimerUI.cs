using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Cook
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
			if (GameManager.Instance.IsCountDownToStartActive())
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
			gameObject.SetActive(true);
		}

		private void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}
