using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public class ContainerCounter : BaseCounter
	{
		public event EventHandler OnPlayerGrabbedObject;

		[SerializeField] private KitchenObjectSO _kitchenObjectSO;

		public override void Interact(PlayerController player)
		{
			if (!player.HasKitchenObject())
			{
				KitchenObject.SpawnKitchenObject(_kitchenObjectSO, player);
				OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
			}
			else
			{
				if (!player.GetKitchenObject().TryGetPlate(out var plateKitchenObject)) return;
				if (plateKitchenObject.TryAddIngredient(_kitchenObjectSO))
				{
					// GetKitchenObject().DestroySelf();
				};
			}
		}
	}
}
