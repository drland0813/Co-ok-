using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.Cook
{
	public class PlateIconSingleUI : MonoBehaviour
	{
		[SerializeField] private Image _icon;

		public void SetKitchenObjectIcon(KitchenObjectSO kitchenObjectSO)
		{
			_icon.sprite = kitchenObjectSO.Sprite;
		}

	}
}
