using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Drland.Cook
{
	enum PlateCounterState
	{
		None,
		HasOnePlate,
		HasMoreThanOnePlate
	}
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

		public void SpawnDirtyPlate()
		{
			SpawnPlate();
		}

		public override void Interact(PlayerController player)
		{
			if (player.HasKitchenObject()) return;

			PlateCounterState stateCounter;
			if (_plateSpawnedAmount >= (int) PlateCounterState.HasMoreThanOnePlate)
			{
				stateCounter = PlateCounterState.HasMoreThanOnePlate;
			}
			else
			{
				stateCounter = (PlateCounterState) _plateSpawnedAmount;
			}
			switch (stateCounter)
			{
				case PlateCounterState.None:
					return;
				case PlateCounterState.HasOnePlate:
					SpawnPlateToPlayer(player);
					break;
				case PlateCounterState.HasMoreThanOnePlate:
				{
					var dirtyPlateStack = KitchenObject.SpawnKitchenObject(_dirtyPlateSO, player) as DirtyPlateStackKitchenObject;
					if (dirtyPlateStack) dirtyPlateStack.InitStack(_plateSpawnedAmount);
					RemovePlates(_plateSpawnedAmount);
					break;
				}
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}