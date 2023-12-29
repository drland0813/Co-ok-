using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Drland.Cook
{
	public class PlayerUI : MonoBehaviour
	{
		[SerializeField] private Button _interactButton;
		[SerializeField] private Button _interactAlternateButton;
		
		public void EnableInteractButton(bool enable)
		{
			_interactButton.gameObject.SetActive(enable);
		}

		public void EnableInteractAlternateButton(bool enable)
		{
			_interactAlternateButton.gameObject.SetActive(enable);
		}
	}
}