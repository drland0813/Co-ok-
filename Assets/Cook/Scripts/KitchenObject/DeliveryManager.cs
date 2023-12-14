using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Drland.Cook
{
	public class DeliveryManager : Singleton<DeliveryManager>
	{
		public event EventHandler OnRecipeSpawned;
		public event EventHandler OnRecipeCompleted;

		[SerializeField] private RecipeListSO _recipeListSO;
		[SerializeField] private float _spawnRecipeTimer;
		[SerializeField] private float _spawnRecipeTimerMax;
		[SerializeField] private int _waitingRecipeMax;
		private List<RecipeSO> _waitingRecipeSOList;

		private int _successfulRecipesAmount = 0;

		protected override void Awake()
		{
			base.Awake();
			_waitingRecipeSOList = new List<RecipeSO>();
		}

		private void Start()
		{
			StartCoroutine(StartReceivingOrdersCoroutine());
		}

		private IEnumerator StartReceivingOrdersCoroutine()
		{
			while (_spawnRecipeTimer > 0)
			{
				_spawnRecipeTimer -= Time.deltaTime;
				if (_spawnRecipeTimer <= 0f)
				{
					_spawnRecipeTimer = _spawnRecipeTimerMax;

					if (_waitingRecipeSOList.Count < _waitingRecipeMax)
					{
						var waitingRecipeSO = _recipeListSO.Recipes[UnityEngine.Random.Range(0, _recipeListSO.Recipes.Count)];
						_waitingRecipeSOList.Add(waitingRecipeSO);

						OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
					}
				}
				yield return null;
			}
		}

		public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
		{
			for (var i = 0; i < _waitingRecipeSOList.Count; i++)
			{
				var watingRecipeSO = _waitingRecipeSOList[i];

				if (watingRecipeSO.KitchenObjectSOList.Count != plateKitchenObject.GetKitchenObjectSOList().Count) continue;
				var plateContentMatchesRecipe = true;
				foreach (var recipeKitchenObjectSO in watingRecipeSO.KitchenObjectSOList)
				{
					var ingredientFound = plateKitchenObject.GetKitchenObjectSOList().Any(plateKitchenObjectSO => plateKitchenObjectSO == recipeKitchenObjectSO);
					if (!ingredientFound)
					{
						plateContentMatchesRecipe = false;
					}
				}

				if (!plateContentMatchesRecipe) continue;
				_successfulRecipesAmount++;
				_waitingRecipeSOList.RemoveAt(i);
				OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
				SoundManager.Instance.PlaySound(SoundType.DeliverySuccess, transform);
				return;
			}
			SoundManager.Instance.PlaySound(SoundType.DeliveryFail, transform);
		}

		public List<RecipeSO> GetWaitingRecipeSOList()
		{
			return _waitingRecipeSOList;
		}

		public int GetSuccessfulRecipesAmount()
		{
			return _successfulRecipesAmount;
		}
	}
}