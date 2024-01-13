using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Drland.Cook
{
	public class GameOverUI : UIController
	{
		[SerializeField] private TextMeshProUGUI _recipeDeliveredCountText;
		

		private void SetRecipeDeliveredText()
		{
			_recipeDeliveredCountText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
		}

		public override void Initialize()
		{
			
		}

		public override void Show()
		{
			base.Show();
			SetRecipeDeliveredText();
		}
	}
}
