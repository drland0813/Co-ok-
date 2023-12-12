using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{

	public class PlayerController : MonoBehaviour, IKitchenObjectParent
	{
		public static PlayerController Instance { get; private set; }

		public event EventHandler<OnSelectedCounterChangedArgs> OnSelectedCounterChanged;
		public class OnSelectedCounterChangedArgs : EventArgs
		{
			public BaseCounter SelectedCounter;
		}

		[SerializeField] private float _moveSpeed;
		[SerializeField] private float _rotateSpeed;

		[SerializeField] private GameInput _gameInput;
		[SerializeField] private PlayerUI _playerUI;
		[SerializeField] private Transform _kitchenObjectHoldPoint;


		private bool _isWalking;
		private Vector3 _lastInteractPosition;
		private BaseCounter _selectedCounter;

		private KitchenObject _currentKitchenObject;


		private void Awake()
		{
			Application.targetFrameRate = 60;

			if (Instance != null)
			{
				Debug.LogError("There is more than one Player instance");
			}
			Instance = this;
			_playerUI.RegisterInteractObjectCallback(OnHandleInteractions);
			_playerUI.RegisterInteractAlternateObjectCallback(OnHanldeInteractAlternate);

		}

		private void Update()
		{
			HandleMovement();
			HandleInteractions();
		}

		private void HandleMovement()
		{
			Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
			Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

			float moveDistance = _moveSpeed * Time.deltaTime;
			float playerRadius = 0.7f;
			float playerHeight = 2f;

			bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

			_isWalking = moveDir != Vector3.zero;

			if (!canMove)
			{
				Vector3 moveDirX = new Vector3(moveDir.x, 0, 0);
				canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

				if (canMove)
				{
					moveDir = moveDirX;
				}
				else
				{
					Vector3 moveDirZ = new Vector3(0, 0, moveDir.z);
					canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

					if (canMove)
					{
						moveDir = moveDirZ;
					}
				}
			}

			if (canMove)
			{
				transform.position += moveDir * moveDistance;
			}
			transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _rotateSpeed);
		}

		private void HandleInteractions()
		{
			Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
			Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

			if (moveDir != Vector3.zero)
				_lastInteractPosition = moveDir;

			float interactDistance = 2f;
			if (Physics.Raycast(transform.position, _lastInteractPosition, out RaycastHit raycastHit, interactDistance))
			{
				if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
				{
					if (baseCounter != _selectedCounter)
					{
						SetSelectedCounter(baseCounter);
					}
				}
				else
				{
					SetSelectedCounter(null);
				}
			}
			else
			{
				SetSelectedCounter(null);
			}
		}

		private void OnHandleInteractions()
		{
			if (!GameManager.Instance.IsGamePlaying()) return;

			_selectedCounter?.Interact(this);
		}

		private void OnHanldeInteractAlternate()
		{
			if (!GameManager.Instance.IsGamePlaying()) return;

			_selectedCounter?.InteractAlternate(this);
		}

		private void SetSelectedCounter(BaseCounter selectedCounter)
		{
			this._selectedCounter = selectedCounter;
			OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedArgs
			{
				SelectedCounter = selectedCounter
			});

			bool isInteractCuttingCounter = selectedCounter as CuttingCounter;
			_playerUI.EnableInteractAlternateButton(isInteractCuttingCounter);

		}

		public bool IsWalking()
		{
			return _isWalking;
		}

		public Transform GetKitchenObjectFollowTransform()
		{
			return _kitchenObjectHoldPoint;
		}

		public void SetKitchenObject(KitchenObject kitchenObject)
		{
			_currentKitchenObject = kitchenObject;
			if (kitchenObject)
				SoundManager.Instance.PlaySound(SoundType.ObjectPickup, transform);
		}

		public KitchenObject GetKitchenObject()
		{
			return _currentKitchenObject;
		}

		public void ClearKitchenObject()
		{
			_currentKitchenObject = null;
		}

		public bool HasKitchenObject()
		{
			return _currentKitchenObject != null;
		}
	}

}