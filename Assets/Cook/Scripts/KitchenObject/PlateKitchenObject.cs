using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public class PlateKitchenObject : KitchenObject
	{
		public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
		public class OnIngredientAddedEventArgs : EventArgs
		{
			public KitchenObjectSO KitchenObjectSO;
		}

		[SerializeField] private List<KitchenObjectSO> _validKitchenObjectSOList;

		private List<KitchenObjectSO> _kitchenObjectSOList;

		private void Awake()
		{
			_kitchenObjectSOList = new List<KitchenObjectSO>();
		}

		public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
		{
			if (!_validKitchenObjectSOList.Contains(kitchenObjectSO))
			{
				return false;
			}
			if (_kitchenObjectSOList.Contains(kitchenObjectSO))
			{
				return false;
			}

			_kitchenObjectSOList.Add(kitchenObjectSO);
			OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
			{
				KitchenObjectSO = kitchenObjectSO
			});
			return true;
		}
		
		public bool CheckIngredientIsValid(KitchenObjectSO kitchenObjectSO)
		{
			return _validKitchenObjectSOList.Contains(kitchenObjectSO);
		}


		public List<KitchenObjectSO> GetKitchenObjectSOList()
		{
			return _kitchenObjectSOList;
		}
	}
}