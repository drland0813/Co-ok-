using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public class PlateIconsUI : MonoBehaviour
	{
		[SerializeField] private PlateKitchenObject _plateKitchenObject;
		[SerializeField] private Transform _iconTemplate;
		[SerializeField] private Transform _iconContainer;

		private void Awake()
		{
			_plateKitchenObject.OnIngredientAdded += Plate_OnIngredientAdded;
			_iconTemplate.gameObject.SetActive(false);
		}

		private void Plate_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
		{
			UpdateVisual();
		}

		private void UpdateVisual()
		{
			foreach (Transform child in _iconContainer)
			{
				if (child == _iconTemplate) continue;
				Destroy(child.gameObject);
			}

			foreach (var item in _plateKitchenObject.GetKitchenObjectSOList())
			{
				var iconTransform = Instantiate(_iconTemplate, _iconContainer);
				iconTransform.gameObject.SetActive(true);
				iconTransform.GetComponent<KitchenObjectUI>().SetupUI(item);
			}
		}
	}
}
