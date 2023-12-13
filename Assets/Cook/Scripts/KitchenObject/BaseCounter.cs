using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
	{
		[SerializeField] private Transform _counterTopPoint;

		protected KitchenObject _currentKitchenObject;

		public abstract void Interact(PlayerController player);

		public virtual void InteractAlternate(PlayerController player) { }

		public Transform GetKitchenObjectFollowTransform()
		{
			return _counterTopPoint;
		}

		public void SetKitchenObject(KitchenObject kitchenObject)
		{
			_currentKitchenObject = kitchenObject;
			if (kitchenObject)
				SoundManager.Instance.PlaySound(SoundType.ObjectDrop, transform);
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
