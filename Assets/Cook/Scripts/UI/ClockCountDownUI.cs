using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.Cook
{
	public class ClockCountDownUI : MonoBehaviour
	{
		[SerializeField] private Image _clockImg;

		[SerializeField] private Color _safeTimeColor;
		[SerializeField] private Color _fasterTimeColor;
		[SerializeField] private Color _dangerTimeColor;

		private Color _clockColor;

		private void Awake()
		{
			_clockColor = _clockImg.color;
		}

		private void Start()
		{
			StartCoroutine(OnStart());
		}

		private IEnumerator OnStart()
		{
			while (!GameManager.Instance.IsGameOver())
			{
				if (GameManager.Instance.IsGamePlaying())
				{
					var timeRemain = GameManager.Instance.GetGamePlayingTimerNormalized();
					_clockColor = timeRemain switch
					{
						< 0.5f and > 0.25f => _fasterTimeColor,
						< 0.25f => _dangerTimeColor,
						_ => _clockColor
					};

					_clockColor.a = 1f;
					_clockImg.color = _clockColor;
					_clockImg.fillAmount = timeRemain;
				}
				yield return null;
			}
		}
	}
}
