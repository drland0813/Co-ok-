using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public class DeliveryManagerUI : MonoBehaviour
	{
		[SerializeField] private Transform _recipeTemplate;
		[SerializeField] private Transform _recipeContainer;
		[SerializeField] private ContentSizeFitterRefresh _contentFitterRefresh;


		private void Awake()
		{
			_recipeTemplate.gameObject.SetActive(false);
		}

		private void Start()
		{
			DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
			DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;

			UpdateVisual();
		}

		private void DeliveryManager_OnRecipeCompleted(object sender, EventArgs e)
		{
			UpdateVisual();
		}

		private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e)
		{
			UpdateVisual();
		}

		public void UpdateVisual()
		{
			foreach (Transform child in _recipeContainer)
			{
				if (child == _recipeTemplate) continue;
				Destroy(child.gameObject);
			}


			foreach (var recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
			{
				var recipeUI = Instantiate(_recipeTemplate, _recipeContainer).GetComponent<DeliveryManagerSingleUI>();
				recipeUI.SetUpUI(recipeSO);
				recipeUI.gameObject.SetActive(true);
			}

			_contentFitterRefresh.RefreshContentFitters();
		}

	}
}

