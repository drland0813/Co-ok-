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
		
		[Header("Visual")]
		[SerializeField] private Material _dirtyMaterial;
		[SerializeField] private MeshRenderer _meshRenderer;


		[SerializeField] private List<KitchenObjectSO> _validKitchenObjectSOList;

		private List<KitchenObjectSO> _kitchenObjectSOList;

		[SerializeField] private bool _isDirty;
		private Material _cleanMaterial;

		private void Awake()
		{
			_kitchenObjectSOList = new List<KitchenObjectSO>();
			_cleanMaterial = _meshRenderer.sharedMaterial;
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

		private void Update()
		{
			SetDirtyVisual(_isDirty);
		}

		public bool CheckIngredientIsValid(KitchenObjectSO kitchenObjectSO)
		{
			return _validKitchenObjectSOList.Contains(kitchenObjectSO);
		}

		public void SetDirtyVisual(bool enable)
		{
			var plateMaterial = enable ? _dirtyMaterial : _cleanMaterial;
			_meshRenderer.sharedMaterial = plateMaterial;
		}


		public List<KitchenObjectSO> GetKitchenObjectSOList()
		{
			return _kitchenObjectSOList;
		}
	}
}