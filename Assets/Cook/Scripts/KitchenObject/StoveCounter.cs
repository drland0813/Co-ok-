using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Drland.Cook
{
	public class StoveCounter : BaseCounter, IHasProgress
	{
		public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

		public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
		public class OnStateChangedEventArgs : EventArgs
		{
			public State State;
		}

		public enum State
		{
			Idle,
			Frying,
			Fried,
			Burned
		}

		[SerializeField] private FryingRecipeSO[] _fryingRecipeSOArray;
		[SerializeField] private BurningRecipeSO[] _burningRecipeSOArray;

		private float _fryingTimer;
		private float _burningTimer;

		private State _state;
		private FryingRecipeSO _fryingRecipeSO;
		private BurningRecipeSO _burningRecipeSO;


		private void Start()
		{
			_state = State.Idle;
			StartCoroutine(WorkingCoroutine());
		}

		private IEnumerator WorkingCoroutine()
		{
			while (!GameManager.Instance.IsGameOver())
			{
				if (HasKitchenObject())
				{
					if (_state == State.Frying)
					{
						_fryingTimer += Time.deltaTime;
						OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
						{
							ProgessNomarlized = (float)_fryingTimer / _fryingRecipeSO.FryingTimeMax
						});

						if (_fryingTimer > _fryingRecipeSO.FryingTimeMax)
						{
							GetKitchenObject().DestroySelf();
							KitchenObject.SpawnKitchenObject(_fryingRecipeSO.Output, this);
							_burningTimer = 0f;
							_state = State.Fried;
							_burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

							OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
							{
								State = _state
							});
						}
					}
					if (_state == State.Fried)
					{
						_burningTimer += Time.deltaTime;
						OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
						{
							ProgessNomarlized = (float)_burningTimer / _burningRecipeSO.BurningTimeMax
						});

						if (_burningTimer > _burningRecipeSO.BurningTimeMax)
						{
							GetKitchenObject().DestroySelf();

							KitchenObject.SpawnKitchenObject(_burningRecipeSO.Output, this);
							_state = State.Burned;

							OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
							{
								State = _state
							});

							OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
							{
								ProgessNomarlized = 0f
							});
						}
					}
				}
				yield return null;
			}
		}

		public override void Interact(PlayerController player)
		{
			if (!HasKitchenObject())
			{
				if (!player.HasKitchenObject()) return;
				if (!HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) return;
				
				player.GetKitchenObject().SetKitchenObjectParent(this);
				_fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
				_state = State.Frying;
				_fryingTimer = 0f;

				OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
				{
					State = _state
				});
				OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
				{
					ProgessNomarlized = (float)_fryingTimer / _fryingRecipeSO.FryingTimeMax
				});
			}
			else
			{
				if (player.HasKitchenObject())
				{
					if (!player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) return;
					
					if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
					{
						GetKitchenObject().DestroySelf();

						_state = State.Idle;
						OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
						{
							State = _state
						});

						OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
						{
							ProgessNomarlized = 0f
						});
					};
				}
				else
				{
					GetKitchenObject().SetKitchenObjectParent(player);
					_state = State.Idle;
					OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
					{
						State = _state
					});

					OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
					{
						ProgessNomarlized = 0f
					});
				}
			}
		}

		private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
		{
			return GetFryingRecipeSOWithInput(kitchenObjectSO) != null;

		}

		private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
		{
			var outputKitchenObjectSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO)?.Output;
			return outputKitchenObjectSO;
		}

		private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO kitchenObjectSO)
		{
			return _fryingRecipeSOArray.FirstOrDefault(i => i.Input == kitchenObjectSO);
		}

		private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO kitchenObjectSO)
		{
			return _burningRecipeSOArray.FirstOrDefault(i => i.Input == kitchenObjectSO);
		}
	}
}
