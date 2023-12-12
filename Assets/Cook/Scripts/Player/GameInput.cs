using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	public class GameInput : MonoBehaviour
	{
		[SerializeField] private FloatingJoystick _joyStick;
		private PlayerAction _inputActions;

		private void Awake()
		{
			_inputActions = new PlayerAction();
			_inputActions.Player.Enable();
		}


		public Vector2 GetMovementVectorNormalized()
		{
			Vector2 inputVector = new Vector2(_joyStick.Horizontal, _joyStick.Vertical);
			if (!GameManager.Instance.IsGamePlaying())
			{
				inputVector = Vector2.zero;
			}
			return inputVector.normalized;

		}
	}
}
