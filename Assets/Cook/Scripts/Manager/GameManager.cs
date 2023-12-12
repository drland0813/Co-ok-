using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	public class GameManager : Singleton<GameManager>
	{
		public event EventHandler OnStateChanged;

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
						OnStateChanged?.Invoke(this, EventArgs.Empty);
					}
					break;
				case GameState.CountDownToStart:
					_countDownToStartTimer -= Time.deltaTime;
					if (_countDownToStartTimer < 0f)
					{
						_gamePlayingTimer = _gamePlayingTimerMax;
						_gameState = GameState.GamePlaying;
						OnStateChanged?.Invoke(this, EventArgs.Empty);
					}
					break;
				case GameState.GamePlaying:
					_gamePlayingTimer -= Time.deltaTime;
					if (_gamePlayingTimer < 0f)
					{
						_gameState = GameState.GameOver;
						OnStateChanged?.Invoke(this, EventArgs.Empty);
					}
					break;
				case GameState.GameOver:
					break;
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

		public bool IsCountDownToStartActive()
		{
			return _gameState == GameState.CountDownToStart;
		}

		public float GetCountDownTimer()
		{
			return _countDownToStartTimer;
		}

		public float GetGamePlayingTimerNomarlized()
		{
			return _gamePlayingTimer / _gamePlayingTimerMax;
		}
	}
}
