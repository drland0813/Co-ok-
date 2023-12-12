using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	public class DeliveryManager : Singleton<DeliveryManager>
	{
		public event EventHandler OnRecipeSpawned;
		public event EventHandler OnRecipeCompleted;

		[SerializeField] private RecipeListSO _recipeListSO;
		[SerializeField] private float _spawnRecipeTimer;
		[SerializeField] private float _spawnRecipeTimerMax;
		[SerializeField] private int _watingRecipeMax;
		private List<RecipeSO> _waitingRecipeSOList;

		private int _successfulRecipesAmount = 0;

		protected override void Awake()
		{
			base.Awake();
			_waitingRecipeSOList = new List<RecipeSO>();
		}

		private void Start()
		{
			StartCoroutine(StartRecievingOrders());
		}

		IEnumerator StartRecievingOrders()
		{
			while (_spawnRecipeTimer > 0)
			{
				_spawnRecipeTimer -= Time.deltaTime;
				if (_spawnRecipeTimer <= 0f)
				{
					_spawnRecipeTimer = _spawnRecipeTimerMax;

					if (_waitingRecipeSOList.Count < _watingRecipeMax)
					{
						RecipeSO waitingRecipeSO = _recipeListSO.Recipes[UnityEngine.Random.Range(0, _recipeListSO.Recipes.Count)];
						_waitingRecipeSOList.Add(waitingRecipeSO);

						OnRecipeSpawned.Invoke(this, EventArgs.Empty);
					}
				}
				yield return null;
			}
		}

		public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
		{
			for (int i = 0; i < _waitingRecipeSOList.Count; i++)
			{
				RecipeSO watingRecipeSO = _waitingRecipeSOList[i];

				if (watingRecipeSO.KitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
				{
					bool plateContentMatchesRecipe = true;
					foreach (KitchenObjectSO recipeKitchenObjectSO in watingRecipeSO.KitchenObjectSOList)
					{
						bool ingredientFound = false;
						foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
						{
							if (plateKitchenObjectSO == recipeKitchenObjectSO)
							{
								ingredientFound = true;
								break;
							}
						}
						if (!ingredientFound)
						{
							plateContentMatchesRecipe = false;
						}
					}
					if (plateContentMatchesRecipe)
					{
						_successfulRecipesAmount++;
						_waitingRecipeSOList.RemoveAt(i);
						OnRecipeCompleted.Invoke(this, EventArgs.Empty);
						SoundManager.Instance.PlaySound(SoundType.DeliverySuccess, transform);
						return;
					}
				}
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