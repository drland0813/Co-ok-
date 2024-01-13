using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.Cook
{
	public class ClockCountDownUI : MonoBehaviour, ICountDownable
	{
		[SerializeField] private Image _clockImg;

		[SerializeField] private Color _safeTimeColor;
		[SerializeField] private Color _fasterTimeColor;
		[SerializeField] private Color _dangerTimeColor;

		private Color _clockColor;
		private Color _tempClockColor;
		private bool _flagToCheckTransition;
		
		public void UpdateTimer(float timer)
		{
			Color clockColor;
			switch (timer)
			{
				case < 0.5f and > 0.25f:
				{
					if (_flagToCheckTransition)
					{
						_tempClockColor = _fasterTimeColor;
						_flagToCheckTransition = false;
					}

					clockColor = _tempClockColor;
					break;
				}
				case < 0.25f:
				{
					if (!_flagToCheckTransition)
					{
						_tempClockColor = _dangerTimeColor;
						_flagToCheckTransition = true;
					}

					clockColor = _tempClockColor;
					break;
				}
				default:
				{
					if (!_flagToCheckTransition)
					{
						_tempClockColor = _safeTimeColor;
						_flagToCheckTransition = true;
					}
					clockColor = _tempClockColor;
					break;
				}
			}

			ChangColor(clockColor);
			_clockImg.fillAmount = timer;
		}

		private void ChangColor(Color color)
		{
			if (color == _clockColor) return;
			
			_clockColor = color;
			_clockImg.color = _clockColor;
		}
	}
}
