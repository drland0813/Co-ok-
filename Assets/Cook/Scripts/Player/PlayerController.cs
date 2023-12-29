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
		
		[Header("Interact")]
		[SerializeField] private float _interactRadius;
		[SerializeField] private float _interactDistance;
		[SerializeField] private float _height;


		[SerializeField] private GameInput _gameInput;
		[SerializeField] private PlayerUI _playerUI;
		
		[Header("Animation Rigging")]		
		[SerializeField] private ArmsRiggingController _armsRiggingController;
		[SerializeField] private List<KitchenObjectHolder> _kitchenObjectHolderList;
		[SerializeField] private RigBuilder _rigBuilder;


		private bool _isWalking;
		private Vector3 _lastInteractPosition;
		private BaseCounter _selectedCounter;

		private KitchenObject _currentKitchenObject;
		private KitchenObjectDataRigging _kitchenObjectDataRigging;


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

			if (!canMove)
			{
				var moveDirX = new Vector3(moveDir.x, 0, 0);
				canMove = moveDir.x != 0 && !Physics.CapsuleCast(position, position + Vector3.up * _height, _interactRadius, moveDirX, moveDistance);

				if (canMove)
				{
					moveDir = moveDirX;
				}
				else
				{
					var moveDirZ = new Vector3(0, 0, moveDir.z);
					canMove = moveDir.z != 0 && !Physics.CapsuleCast(position, position + Vector3.up * _height, _interactRadius, moveDirZ, moveDistance);

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

		private void OnHandleInteractions()
		{
			if (!GameManager.Instance.IsGamePlaying()) return;

			_selectedCounter?.Interact(this);
		}

		private void OnHandeInteractAlternate()
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

			var enableInteractAlternateButton = selectedCounter as CuttingCounter || selectedCounter as SinkCounter;
			_playerUI.EnableInteractAlternateButton(enableInteractAlternateButton);

		}

		private Transform GetKitchenObjectHolder()
		{
			var type = _currentKitchenObject.GetKitchenObjectSO().Type;
			var kitchenObjectHolder = _kitchenObjectHolderList.FirstOrDefault(k => k.Type == type);
			return kitchenObjectHolder.Holder;
		}

		private void EnableAnimationRigging(bool enable)
		{
			_rigBuilder.enabled = false;
			if (!enable)
			{
				_armsRiggingController.DisableArmsRigging();
			}
			else
			{
				_kitchenObjectDataRigging = _currentKitchenObject.GetKitchenObjectDataRigging();
				_armsRiggingController.EnableArmsRigging(_kitchenObjectDataRigging);
			}
			_rigBuilder.enabled = true;
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
			EnableAnimationRigging(true);
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
			_kitchenObjectDataRigging = null;
			EnableAnimationRigging(false);
		}

		public bool HasKitchenObject()
		{
			return _currentKitchenObject != null;
		}
	}
}