using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Drland.Cook
{
	public class RecipeUI : MonoBehaviour
	{
		[SerializeField] private IngredientUI _ingredientIconTemplate;
		[SerializeField] private Transform _iconContainer;

		private void Awake()
		{
			_ingredientIconTemplate.gameObject.SetActive(false);
		}

		public void SetUpUI(RecipeSO recipeSO)
		{
			foreach (Transform child in _iconContainer)
			{
				if (child == _ingredientIconTemplate) continue;
				Destroy(child.gameObject);
			}

			foreach (var kitchenObjectSO in recipeSO.KitchenObjectSOList)
			{
				var icon = Instantiate(_ingredientIconTemplate, _iconContainer);
				icon.gameObject.SetActive(true);
				icon.SetupUI(kitchenObjectSO);
			}
		}
	}
}