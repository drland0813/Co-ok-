using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public class GameManager : Singleton<GameManager>
	{
		private enum GameState
		{
			WaitingToStart,
			CountDownToStart,
			GamePlaying,
			GameOver
		}
		private GameState _gameState;

		[SerializeField] private float _waitingToStartTimer = 1f;
		[SerializeField] private float _countDownToStartTimer = 3f;
		[SerializeField] private float _gamePlayingTimerMax = 10f;

		[SerializeField] private float _gamePlayingTimer;

		[Header("Gameplay UI")]
		[SerializeField] private GameplayUI _gameplayUI;

		protected override void Awake()
		{
			base.Awake();
			_gameState = GameState.WaitingToStart;
		}

		private void Update()
		{
			switch (_gameState)
			{
				case GameState.WaitingToStart:
					_waitingToStartTimer -= Time.deltaTime;
					if (_waitingToStartTimer < 0f)
					{
						_gameState = GameState.CountDownToStart;
					}
					break;
				case GameState.CountDownToStart:
					_countDownToStartTimer -= Time.deltaTime;
					_gameplayUI.UpdateCountDownTimer(_countDownToStartTimer);
					if (_countDownToStartTimer < 0f)
					{
						_gamePlayingTimer = _gamePlayingTimerMax;
						_gameState = GameState.GamePlaying;
					}
					break;
				case GameState.GamePlaying:
					_gamePlayingTimer -= Time.deltaTime;
					_gameplayUI.UpdateClockCountDownTimer(GetGamePlayingTimerNormalized());
					if (_gamePlayingTimer < 0f)
					{
						_gameState = GameState.GameOver;
					}
					break;
				case GameState.GameOver:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public bool IsGamePlaying()
		{
			return _gameState == GameState.GamePlaying;
		}

		public bool IsGameOver()
		{
			return _gameState == GameState.GameOver;
		}

		private float GetGamePlayingTimerNormalized()
		{
			return _gamePlayingTimer / _gamePlayingTimerMax;
		}
	}
}
