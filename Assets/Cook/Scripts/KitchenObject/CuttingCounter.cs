using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Drland.Cook
{
	public class CuttingCounter : BaseCounter, IHasProgress
	{
		public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
		public event EventHandler OnCut;
		
		[SerializeField] private CuttingRecipeSO[] _cuttingRecipeSOArray;

		private int _cuttingProgress;

		public override void Interact(PlayerController player)
		{
			if (!HasKitchenObject())
			{
				if (!player.HasKitchenObject()) return;
				if (!HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) return;
				player.GetKitchenObject().SetKitchenObjectParent(this);
				_cuttingProgress = 0;
				var cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
				{
					ProgressNormalized = (float)_cuttingProgress / cuttingRecipeSO.CuttingProgressMax
				});
			}
			else
			{
				if (player.HasKitchenObject())
				{
					if (!player.GetKitchenObject().TryGetPlate(out var plateKitchenObject)) return;
					if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
					{
						GetKitchenObject().DestroySelf();
					};
				}
				else
				{
					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
					{
						ProgressNormalized = 0
					});
					GetKitchenObject().SetKitchenObjectParent(player);
				}
			}
		}

		public override void InteractAlternate(PlayerController player)
		{
			player.IsHolding = false;
			var canInteractAlternate = HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());
			if (!canInteractAlternate) return;

			_cuttingProgress++;
			OnCut?.Invoke(this, EventArgs.Empty);
			SoundManager.Instance.PlaySound(SoundType.Chop, transform);
			var cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
			OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
			{
				ProgressNormalized = (float)_cuttingProgress / cuttingRecipeSO.CuttingProgressMax
			});

			if (_cuttingProgress < cuttingRecipeSO.CuttingProgressMax) return;
			var outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
			if (!outputKitchenObjectSO) return;

			GetKitchenObject().DestroySelf();
			KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
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
