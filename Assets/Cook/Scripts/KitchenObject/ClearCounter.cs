using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	public class ClearCounter : BaseCounter
	{
		[SerializeField] private KitchenObjectSO _kitcjhenObjectSO;

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
					if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
					{
						if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
						{
							GetKitchenObject().DestroySelf();
						};
					}
					else
					{
						if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
						{
							if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
							{
								player.GetKitchenObject().DestroySelf();
							};
						}
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
