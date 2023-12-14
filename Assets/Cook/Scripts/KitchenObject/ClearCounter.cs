using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public class ClearCounter : BaseCounter
	{
		public override void Interact(PlayerController player)
		{
			if (!HasKitchenObject())
			{
				if (player.HasKitchenObject())
				{
					player.GetKitchenObject().SetKitchenObjectParent(this);
				}
			}
			else
			{
				if (player.HasKitchenObject())
				{
					if (player.GetKitchenObject().TryGetPlate(out var plateKitchenObject))
					{
						if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
						{
							GetKitchenObject().DestroySelf();
						};
					}
					else
					{
						if (!GetKitchenObject().TryGetPlate(out plateKitchenObject)) return;
						if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
						{
							player.GetKitchenObject().DestroySelf();
						};
					}
				}
				else
				{
					GetKitchenObject().SetKitchenObjectParent(player);
				}
			}
		}
	}
}
