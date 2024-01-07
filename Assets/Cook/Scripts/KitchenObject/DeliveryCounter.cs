using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public class DeliveryCounter : BaseCounter
	{
		[SerializeField] private DirtyPlatesCounter _dirtyPlatesCounter;

		public Action<bool> OnDeliveryResult;
		public override void Interact(PlayerController player)
		{
			if (!player.HasKitchenObject()) return;

			if (!player.GetKitchenObject().TryGetPlate(out var plateKitchenObject)) return;
			var deliveryResult = DeliveryManager.Instance.GetDeliveryRecipeResult(plateKitchenObject);
			OnDeliveryResult?.Invoke(deliveryResult);
			player.GetKitchenObject().DestroySelf();
			_dirtyPlatesCounter.SpawnDirtyPlate();
		}
	}
}