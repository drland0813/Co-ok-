using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	public class DeliveryCounter : BaseCounter
	{
		public override void Interact(PlayerController player)
		{
			if (player.HasKitchenObject()) return;

			if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
			{
				DeliveryManager.Instance.DeliveryRecipe(plateKitchenObject);
				player.GetKitchenObject().DestroySelf();
			}
		}
	}
}