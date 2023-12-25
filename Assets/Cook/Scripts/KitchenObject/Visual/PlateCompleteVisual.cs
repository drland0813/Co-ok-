using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Drland.Cook
{
	public class PlateCompleteVisual : MonoBehaviour
	{
		[System.Serializable]
		public struct KitchetObjetSO_GameObject
		{
			public KitchenObjectSO kitchenObjectSO;
			public GameObject GameObject;
		}

		[SerializeField] private PlateKitchenObject _plateKitchenObject;
		[SerializeField] private List<KitchetObjetSO_GameObject> _kitchenSO_GameObjectList;

		private void Awake()
		{
			_plateKitchenObject.OnIngredientAdded += Plate_OnIngredientAdded;
			foreach (KitchetObjetSO_GameObject kitchenObjetSO_GameObject in _kitchenSO_GameObjectList)
			{
				kitchenObjetSO_GameObject.GameObject.SetActive(false);
			}
		}

		private void Plate_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
		{
			GameObject ingredient = _kitchenSO_GameObjectList.FirstOrDefault(k => k.kitchenObjectSO == e.KitchenObjectSO).GameObject;
			if (ingredient)
			{
				ingredient.SetActive(true);
			}
		}
	}
}
