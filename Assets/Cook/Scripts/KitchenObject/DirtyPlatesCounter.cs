using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Drland.Cook
{
	public class DirtyPlatesCounter : PlatesCounter
	{
		[SerializeField] private KitchenObjectSO _dirtyPlateSO;
		private void RemovePlates(int quantity)
		{
			for (var i = 0; i < quantity; i++)
			{
				RemovePlate();
			}
		}
		protected override IEnumerator SpawnPlatesCoroutine()
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
			if (player.HasKitchenObject()) return;
			switch (_plateSpawnedAmount)
			{
				case 0:
					return;
				case 1:
					SpawnPlateToPlayer(player);
					break;
				default:
				{
					var dirtyPlateStack = KitchenObject.SpawnKitchenObject(_dirtyPlateSO, player) as DirtyPlateStackKitchenObject;
					if (dirtyPlateStack) dirtyPlateStack.InitStack(_plateSpawnedAmount);
					RemovePlates(_plateSpawnedAmount);
					break;
				}
			}
		}
	}
}