using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Drland.Cook
{
	public abstract class PlateHolderCounterBase : BaseCounter, IPlateHolder
	{
		[SerializeField] protected int _plateSpawnAmountMax = 4;
		[SerializeField] protected KitchenObjectSO _plateObjectSO;
		protected PlateKitchenObject _plateKitchenObject;
		protected int _plateSpawnedAmount;
		protected void Awake()
		{
			_plateKitchenObject = _plateObjectSO.Prefab.GetComponent<PlateKitchenObject>();
		}
		
		protected void SpawnPlate()
		{
			_plateSpawnedAmount++;
			OnPlateSpawned?.Invoke(this, EventArgs.Empty);
		}

		protected void RemovePlate()
		{
			_plateSpawnedAmount--;
			OnPlateRemoved?.Invoke(this, EventArgs.Empty);
		}
		
		protected void SpawnPlateWithIngredient(PlayerController player, KitchenObject ingredient)
		{
			_plateSpawnedAmount--;
			var plateKitchenObject = KitchenObject.SpawnKitchenObject(_plateObjectSO, player).GetComponent<PlateKitchenObject>();
			OnPlateRemoved?.Invoke(this, EventArgs.Empty);
			if (plateKitchenObject.TryAddIngredient(ingredient.GetKitchenObjectSO()))
			{
				Destroy(ingredient.gameObject);
			}
		}

		protected void SpawnPlateToPlayer(PlayerController player)
		{
			_plateSpawnedAmount--;
			KitchenObject.SpawnKitchenObject(_plateObjectSO, player);
			OnPlateRemoved?.Invoke(this, EventArgs.Empty);
		}
		public abstract override void Interact(PlayerController player);
		public event EventHandler OnPlateSpawned;
		public event EventHandler OnPlateRemoved;
	}
	
	public class PlatesCounter : PlateHolderCounterBase
	{

		[SerializeField] protected float _spawnPlateTimerMax = 4f;
		[SerializeField] protected bool _immediatelySpawn;
		private float _spawnPlateTimer;

		protected void Start()
		{
			StartCoroutine(SpawnPlatesCoroutine());
		}

		private IEnumerator SpawnPlatesCoroutine()
		{
			if (_immediatelySpawn)
			{
				for (var i = 0; i < _plateSpawnAmountMax; i++)
				{
					SpawnPlate();
					yield return null;
				}
			}
			else
			{
				while (_spawnPlateTimer < _spawnPlateTimerMax)
				{
					_spawnPlateTimer += Time.deltaTime;
					if (_spawnPlateTimer > _spawnPlateTimerMax)
					{
						_spawnPlateTimer = 0f;
						if (_plateSpawnedAmount < _plateSpawnAmountMax)
						{
							SpawnPlate();
						}
					}
					yield return null;
				}
			}
		}

		public override void Interact(PlayerController player)
		{
			if (!player.HasKitchenObject())
			{
				if (!(_plateSpawnedAmount > 0)) return;
				SpawnPlateToPlayer(player);
			}
			else
			{
				var ingredient = player.GetKitchenObject();
				if (!(_plateSpawnedAmount > 0)) return;
				if (!_plateKitchenObject.CheckIngredientIsValid(ingredient.GetKitchenObjectSO())) return;
				SpawnPlateWithIngredient(player, ingredient);
			}
		}
	}
}