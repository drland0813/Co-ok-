using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.Cook
{
	public class KitchenObjectUI : MonoBehaviour
	{
		[SerializeField] protected Image _icon;

		public virtual void SetupUI(KitchenObjectSO kitchenObjectSO)
		{
			_icon.enabled = true;
			_icon.sprite = kitchenObjectSO.Sprite;
		}
		public void Enable(bool enable)
		{
			gameObject.SetActive(enable);
		}
	}
}
