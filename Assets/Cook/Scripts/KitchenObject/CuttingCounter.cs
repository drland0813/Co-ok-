using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cook
{
	public class CuttingCounter : BaseCounter, IHasProgress
	{
		public static event EventHandler OnAnyCut;

		public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

		public event EventHandler OnCut;

		[SerializeField] private CuttingRecipeSO[] _cuttingRecipeSOArray;

		private int _cuttingProgress;

		public override void Interact(PlayerController player)
		{
			if (!HasKitchenObject())
			{
				if (player.HasKitchenObject())
				{
					if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
					{
						player.GetKitchenObject().SetKitchenObjectParent(this);
						_cuttingProgress = 0;
						CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

						OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
						{
							ProgessNomarlized = (float)_cuttingProgress / cuttingRecipeSO.CuttingProgressMax
						});
					}
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
				}
				else
				{
					GetKitchenObject().SetKitchenObjectParent(player);
				}
			}
		}

		public override void InteractAlternate(PlayerController player)
		{
			bool canInteractAlternate = HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());
			if (!canInteractAlternate) return;

			_cuttingProgress++;
			OnCut?.Invoke(this, EventArgs.Empty);
			SoundManager.Instance.PlaySound(SoundType.Chop, transform);
			CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
			OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
			{
				ProgessNomarlized = (float)_cuttingProgress / cuttingRecipeSO.CuttingProgressMax
			});

			if (_cuttingProgress >= cuttingRecipeSO.CuttingProgressMax)
			{
				KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
				if (!outputKitchenObjectSO) return;

				GetKitchenObject().DestroySelf();
				KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
			}
		}

		private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
		{
			return GetCuttingRecipeSOWithInput(kitchenObjectSO) != null;
		}

		private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
		{
			var outputKitchenObjectSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO)?.Output;
			return outputKitchenObjectSO;
		}

		private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO kitchenObjectSO)
		{
			return _cuttingRecipeSOArray.FirstOrDefault(i => i.Input == kitchenObjectSO);
		}
	}
}
