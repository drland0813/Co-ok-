using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.Cook
{
	public class KitchenObjectUI : MonoBehaviour
	{
		[SerializeField] private Image _icon;

		public void SetKitchenObjectIcon(KitchenObjectSO kitchenObjectSO)
		{
			_icon.sprite = kitchenObjectSO.Sprite;
		}
		public void Enable(bool enable)
		{
			gameObject.SetActive(enable);
		}
	}
}
