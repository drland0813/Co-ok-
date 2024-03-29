using System;
using System.Collections;
using System.Collections.Generic;
using Drland.Cook;
using UnityEngine;
using UnityEngine.Serialization;

namespace Drland.Cook
{
    public class SinkCounter : PlateHolderCounterBase, IHasProgress
    {
        public event EventHandler<OnDirtyPlateSpawnedAgrs> OnDirtyPlateSpawned; 
        public class OnDirtyPlateSpawnedAgrs: EventArgs
        {
            public int TotalPlate;
        }

        public Action<bool> OnWashing;
        
        [SerializeField] private float _washingProgressMax;
        [SerializeField] private float _washingProgress;

        private int _dirtyPlatesCount;

        public override void Interact(PlayerController player)
        {
            if (!player.HasKitchenObject())
            {
                if (!(_plateSpawnedAmount > 0)) return;
                SpawnPlateToPlayer(player);
            }
            else
            {
                var kitchenObject = player.GetKitchenObject();
                if (kitchenObject.TryGetPlate(out var plate))
                {
                    if (!plate.IsDirty) return;
                    
                    if (_dirtyPlatesCount >= _plateSpawnAmountMax) return;
                    kitchenObject.DestroySelf();
                    _dirtyPlatesCount++;
                    OnDirtyPlateSpawned?.Invoke(this, new OnDirtyPlateSpawnedAgrs() {TotalPlate = 1});
                }
                else if (kitchenObject is DirtyPlateStackKitchenObject stackKitchenObject)
                {
                    if (_dirtyPlatesCount >= _plateSpawnAmountMax) return;
                    _dirtyPlatesCount += stackKitchenObject.Quantity;
                    OnDirtyPlateSpawned?.Invoke(this, new OnDirtyPlateSpawnedAgrs() {TotalPlate = stackKitchenObject.Quantity});
                    kitchenObject.DestroySelf();
                }
                else
                {
                    var ingredient = kitchenObject;
                    if (!(_plateSpawnedAmount > 0)) return;
                    if (!_plateKitchenObject.CheckIngredientIsValid(ingredient.GetKitchenObjectSO())) return;
                    SpawnPlateWithIngredient(player, ingredient);
                }
            }
        }

        public override void InteractAlternate(PlayerController player)
        {
            if (_dirtyPlatesCount == 0 || player.HasKitchenObject())
            {
                player.IsHolding = false;
                return;
            }

            if (!player.IsHolding)
            {
                _washingProgress = _washingProgress > _washingProgressMax ? 0 : _washingProgress;
                OnWashing?.Invoke(false);
                return;
            }
            StartCoroutine(WashingCoroutine(player));
        }

        private IEnumerator WashingCoroutine(PlayerController player)
        {
            while (player.IsHolding && _washingProgress < _washingProgressMax)
            {
                OnWashing?.Invoke(true);
                _washingProgress += Time.deltaTime;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    ProgressNormalized = _washingProgress / _washingProgressMax
                });
                if (_washingProgress > _washingProgressMax)
                {
                    _dirtyPlatesCount--;
                    SpawnPlate();
                    _washingProgress = 0f;
                }

                if (_dirtyPlatesCount == 0)
                {
                    player.IsHolding = false;
                }
                yield return null;
            }
            OnWashing?.Invoke(false);
        }

        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    }
}
