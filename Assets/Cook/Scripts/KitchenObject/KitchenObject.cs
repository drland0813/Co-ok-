using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	public class KitchenObject : MonoBehaviour
	{
		[SerializeField] private KitchenObjectSO _kitchenObjectSO;

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
			if (_kitchenObjectParent != null) _kitchenObjectParent.ClearKitchenObject();
			
			_kitchenObjectParent = kitchenObjectParent;
			_kitchenObjectParent.SetKitchenObject(this);

			transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
			transform.localPosition = Vector3.zero;
		}

		public void DestroySelf()
		{
			_kitchenObjectParent.ClearKitchenObject();
			Destroy(gameObject);
		}

		public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
		{
			Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab);
			KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
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
			else
			{
				plateKitchenObject = null;
				return false;
			}
		}
	}

}