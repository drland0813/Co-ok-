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
        public event EventHandler<OnDropDirtyPlatesAgrs> OnDropDirtyPlates; 
        public class OnDropDirtyPlatesAgrs: EventArgs
        {
            public int TotalPlate;
        }
        
        [SerializeField] private float _washingProgressMax;
        [SerializeField] private float _washingProgress;

        private int _dirtyPlatesCount;

        private void Awake()
        {
            _dirtyPlatesCount = 4;
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

        public override void InteractAlternate(PlayerController player)
        {
            if (_dirtyPlatesCount == 0) return;
            StartCoroutine(WashingCoroutine());
        }

        private IEnumerator WashingCoroutine()
        {
            _dirtyPlatesCount--;
            _washingProgress = 0f;
            while (_washingProgress < _washingProgressMax)
            {
                _washingProgress += Time.deltaTime;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    ProgessNomarlized = _washingProgress / _washingProgressMax
                });
                yield return null;
            }
            SpawnPlate();
        }

        public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    }
}
