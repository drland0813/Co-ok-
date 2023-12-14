using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.Cook
{
	public class DeliveryManagerSingleUI : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _recipeNameText;
		[SerializeField] private Transform _iconTemplate;
		[SerializeField] private Transform _iconContainer;

		private void Awake()
		{
			_iconTemplate.gameObject.SetActive(false);
		}

		public void SetUpUI(RecipeSO recipeSO)
		{
			_recipeNameText.text = recipeSO.Name;

			foreach (Transform child in _iconContainer)
			{
				if (child == _iconTemplate) continue;
				Destroy(child.gameObject);
			}

			foreach (var kitchenObjectSO in recipeSO.KitchenObjectSOList)
			{
				var icon = Instantiate(_iconTemplate, _iconContainer);
				icon.gameObject.SetActive(true);
				icon.GetComponent<Image>().sprite = kitchenObjectSO.Sprite;
			}
		}
	}
}