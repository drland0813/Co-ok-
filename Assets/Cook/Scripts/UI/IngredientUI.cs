using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Drland.Cook
{
	public class IngredientUI : KitchenObjectUI
	{
		[SerializeField] private Image _bgImg;

		public override void SetupUI(KitchenObjectSO kitchenObjectSO)
		{
			_bgImg.enabled = true;
			base.SetupUI(kitchenObjectSO);
		}
	}
}
