using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	public class PlatesCounter : BaseCounter
	{
		public event EventHandler OnPlateSpawned;
		public event EventHandler OnPlateRemoved;


		[SerializeField] private KitchenObjectSO _plateObjectSO;
		[SerializeField] private float _spawnPlateTimerMax = 4f;
		[SerializeField] private int _plateSpawnAmountMax = 4;

		private float _spawnPlateTimer;
		private float _plateSpawnedAmount;

		private void Start()
		{
			StartCoroutine(SpawnPlates());
		}

		IEnumerator SpawnPlates()
		{
			while (_spawnPlateTimer < _spawnPlateTimerMax)
			{
				_spawnPlateTimer += Time.deltaTime;
				if (_spawnPlateTimer > _spawnPlateTimerMax)
				{
					_spawnPlateTimer = 0f;
					if (_plateSpawnedAmount < _plateSpawnAmountMax)
					{
						_plateSpawnedAmount++;
						OnPlateSpawned?.Invoke(this, EventArgs.Empty);
					}
				}
				yield return null;
			}
		}

		public override void Interact(PlayerController player)
		{
			if (!player.HasKitchenObject())
			{
				if (_plateSpawnedAmount > 0)
				{
					_plateSpawnedAmount--;
					KitchenObject.SpawnKitchenObject(_plateObjectSO, player);
					OnPlateRemoved?.Invoke(this, EventArgs.Empty);
				}
			}
			else
			{
				KitchenObject ingredient = player.GetKitchenObject();
				if (_plateSpawnedAmount > 0)
				{
					_plateSpawnedAmount--;
					PlateKitchenObject plateKitchenObject = KitchenObject.SpawnKitchenObject(_plateObjectSO, player).GetComponent<PlateKitchenObject>();
					OnPlateRemoved?.Invoke(this, EventArgs.Empty);
					if (plateKitchenObject.TryAddIngredient(ingredient.GetKitchenObjectSO()))
					{
						Destroy(ingredient.gameObject);
					};
				}
			}
		}
	}

}