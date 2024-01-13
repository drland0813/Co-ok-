using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Serialization;

namespace Drland.Cook
{

	public class PlayerController : MonoBehaviour, IKitchenObjectParent
	{
		public static PlayerController Instance { get; private set; }

		public event EventHandler<OnSelectedCounterChangedArgs> OnSelectedCounterChanged;
		public class OnSelectedCounterChangedArgs : EventArgs
		{
			public BaseCounter SelectedCounter;
		}

		[Header("Movement")]
		[SerializeField] private float _moveSpeed;
		[SerializeField] private float _rotateSpeed;
		[SerializeField] private float _limitMoveValue;

		[Header("Interact")]
		[SerializeField] private float _interactRadius;
		[SerializeField] private float _interactDistance;
		[SerializeField] private float _height;


		[SerializeField] private GameInput _gameInput;
		[SerializeField] private PlayerUI _playerUI;

		[Header("Animation Rigging")] 
		[SerializeField] private RigActionManager _rigActionManager;
		[SerializeField] private List<KitchenObjectHolder> _kitchenObjectHolderList;

		[Header("Footstep")] 
		[SerializeField] private float _footStepTimer;
		[SerializeField] private float _footStepTimerMax;
		
		private bool _isWalking;
		private Vector3 _lastInteractPosition;
		private BaseCounter _selectedCounter;
		private KitchenObject _currentKitchenObject;

		private bool _isHolding;

		public bool IsHolding
		{
			get => _isHolding;
			set => _isHolding = value;
		}


		private void Awake()
		{
			Application.targetFrameRate = 60;

			if (Instance != null)
			{
				Debug.LogError("There is more than one Player instance");
			}
			Instance = this;
			_gameInput.RegisterInteractObjectCallback(OnHandleInteractions);
			_gameInput.RegisterInteractAlternateObjectCallback(OnHandeInteractAlternate);

		}

		private void Update()
		{
			HandleAnimationRigging();
			HandleMovement();
			HandleInteractions();
		}

		private void HandleMovement()
		{
			var inputVector = _gameInput.GetMovementVectorNormalized();
			var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
			var moveDistance = _moveSpeed * Time.deltaTime;

			var position = transform.position;
			var canMove = !Physics.CapsuleCast(position, position + Vector3.up * _height, _interactRadius, moveDir, moveDistance);

			_isWalking = moveDir != Vector3.zero;
			if (_isWalking)
			{
				PlayFootstepSound();
				_isHolding = false;
			}

			if (!canMove)
			{
				var moveDirX = new Vector3(moveDir.x, 0, 0);
				canMove = (moveDir.x <-_limitMoveValue || moveDir.x > _limitMoveValue) && !Physics.CapsuleCast(position, position + Vector3.up * _height, _interactRadius, moveDirX, moveDistance);
				if (canMove)
				{
					moveDir = moveDirX;
				}
				else
				{
					var moveDirZ = new Vector3(0, 0, moveDir.z);
					canMove = (moveDir.z <-_limitMoveValue || moveDir.x > _limitMoveValue) && !Physics.CapsuleCast(position, position + Vector3.up * _height, _interactRadius, moveDirZ, moveDistance);
			
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
			var forwardVector =  Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * _rotateSpeed);
			if (forwardVector == Vector3.zero) return;
			transform.forward = forwardVector;
		}

		private void HandleInteractions()
		{
			var inputVector = _gameInput.GetMovementVectorNormalized();
			var moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

			if (moveDir != Vector3.zero)
				_lastInteractPosition = moveDir;

			if (Physics.Raycast(transform.position, _lastInteractPosition, out var raycastHit, _interactDistance))
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

		private void HandleAnimationRigging()
		{
			_rigActionManager.Enable(PlayerActionType.Wash, _isHolding);
		}

		private void OnHandleInteractions()
		{
			if (!GameManager.Instance.IsGamePlaying()) return;

			_selectedCounter?.Interact(this);
		}

		private void OnHandeInteractAlternate(bool isHolding = false)
		{
			if (!GameManager.Instance.IsGamePlaying()) return;

			_isHolding = isHolding;
			_selectedCounter?.InteractAlternate(this);
		}

		private void SetSelectedCounter(BaseCounter selectedCounter)
		{
			this._selectedCounter = selectedCounter;
			OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedArgs
			{
				SelectedCounter = selectedCounter
			});

			var enableInteractAlternateButton = selectedCounter as CuttingCounter || selectedCounter as SinkCounter;

			var actionType = selectedCounter is CuttingCounter ? PlayerActionType.Cut : PlayerActionType.Wash;
			_playerUI.EnableInteractAlternateButton(enableInteractAlternateButton, actionType);

		}

		private Transform GetKitchenObjectHolder()
		{
			var type = _currentKitchenObject.GetKitchenObjectSO().Type;
			var kitchenObjectHolder = _kitchenObjectHolderList.FirstOrDefault(k => k.Type == type);
			return kitchenObjectHolder.Holder;
		}

		private void PlayFootstepSound()
		{
			_footStepTimer -= Time.deltaTime;
			if (!(_footStepTimer < 0)) return;
			
			_footStepTimer = _footStepTimerMax;
			SoundManager.Instance.PlaySound(SoundType.Walking, transform);
		}
		

		public bool IsWalking()
		{
			return _isWalking;
		}

		public Transform GetKitchenObjectFollowTransform()
		{
			return GetKitchenObjectHolder();
		}

		public void SetKitchenObject(KitchenObject kitchenObject)
		{
			_currentKitchenObject = kitchenObject;
			_rigActionManager.Enable(PlayerActionType.Hold ,true);
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
			_rigActionManager.Enable(PlayerActionType.Hold ,false);
		}

		public bool HasKitchenObject()
		{
			return _currentKitchenObject != null;
		}
	}
}