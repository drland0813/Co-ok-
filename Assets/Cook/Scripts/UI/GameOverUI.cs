using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Cook
{
	public class GameOverUI : UIController
	{
		[SerializeField] private TextMeshProUGUI _recipeDeliveredCountText;

		private void Start()
		{
			GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
			// Hide();
		}

		private void GameManager_OnStateChanged(object sender, EventArgs e)
		{
			// if (GameManager.Instance.IsGameOver())
			// {
			// 	Show();
			// 	SetRecipeDeliveredText();
			// }
			// else
			// {
			// 	Hide();
			// }
		}

		private void SetRecipeDeliveredText()
		{
			_recipeDeliveredCountText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
		}

		public override void Initialize()
		{
			throw new NotImplementedException();
		}
	}
}
