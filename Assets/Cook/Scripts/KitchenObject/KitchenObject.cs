using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Drland.Cook
{
	public class KitchenObject : MonoBehaviour
	{
		[SerializeField] private KitchenObjectSO _kitchenObjectSO;
		
		[CanBeNull]
		[SerializeField] private KitchenObjectUI _kitchenObjectUI;

		private IKitchenObjectParent _kitchenObjectParent;

		public KitchenObjectSO GetKitchenObjectSO()
		{
			return _kitchenObjectSO;
		}

		public IKitchenObjectParent GetKitchenObjectParent()
		{
			return _kitchenObjectParent;
		}

		public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
		{
			_kitchenObjectParent?.ClearKitchenObject();

			_kitchenObjectParent = kitchenObjectParent;
			_kitchenObjectParent.SetKitchenObject(this);

			transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
			transform.localPosition = Vector3.zero;
			transform.localEulerAngles = Vector3.zero;

			if (kitchenObjectParent is not StoveCounter)
			{
				EnableKitchenObjectUI(_kitchenObjectSO.IsIngredient); 
			}

		}

		public void DestroySelf()
		{
			_kitchenObjectParent.ClearKitchenObject();
			Destroy(gameObject);
		}

		public void EnableKitchenObjectUI(bool enable)
		{
			if (!_kitchenObjectUI) return;
			_kitchenObjectUI.Enable(enable);
			if (!enable) return;
			_kitchenObjectUI.SetKitchenObjectIcon(_kitchenObjectSO);
		}

		public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
		{
			var kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab);
			var kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
			kitchenObject.EnableKitchenObjectUI(kitchenObjectSO.IsIngredient); 
			kitchenObject.SetKitchenObjectParent(kitchenObjectParent);  
			
			return kitchenObject;
		}

		public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
		{
			if (this is PlateKitchenObject)
			{
				plateKitchenObject = this as PlateKitchenObject;
				return true;
			}
			plateKitchenObject = null;
			return false;
		}
	}

}