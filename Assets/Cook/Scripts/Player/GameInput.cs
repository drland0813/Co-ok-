using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

namespace Drland.Cook
{
	public class GameInput : MonoBehaviour
	{
		[SerializeField] private FloatingJoystick _joyStick;
		private PlayerAction _inputActions;
		
		private Action _onInteractObject;
		private Action _onInteractAlternateObject;

		private void Awake()
		{
			_inputActions = new PlayerAction();
			_inputActions.Player.Enable();
			_inputActions.Player.Interact.performed += InteractPerformed;
			_inputActions.Player.InteractAlternate.performed += InteractAlternatePerformed;
		}

		private void InteractPerformed(InputAction.CallbackContext obj)
		{
			_onInteractObject?.Invoke();
		}
		private void InteractAlternatePerformed(InputAction.CallbackContext obj)
		{
			_onInteractAlternateObject?.Invoke();
		}

		public Vector2 GetMovementVectorNormalized()
		{
			var inputVector = new Vector2();
#if  UNITY_EDITOR
			inputVector = _inputActions.Player.PCMove.ReadValue<Vector2>();
#else
			inputVector = new Vector2(_joyStick.Horizontal, _joyStick.Vertical);
#endif
			if (!GameManager.Instance.IsGamePlaying())
			{
				inputVector = Vector2.zero;
			}
			return inputVector.normalized;
		}

		public void RegisterInteractObjectCallback(Action interactObjectAction)
		{
			_onInteractObject = interactObjectAction;
		}

		public void RegisterInteractAlternateObjectCallback(Action interactAlternateObjectAction)
		{
			_onInteractAlternateObject = interactAlternateObjectAction;
		}
	}
}
