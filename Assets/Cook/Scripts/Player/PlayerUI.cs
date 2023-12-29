using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Drland.Cook
{
	public enum UIActionType
	{
		Cut,
		Wash,
		Cook
	}

	[Serializable]
	public class UIAction
	{
		public UIActionType Type;
		public Sprite Icon;
	}
	public class PlayerUI : MonoBehaviour
	{
		[SerializeField] private Button _interactButton;
		[SerializeField] private Button _interactAlternateButton;
		[SerializeField] private Image _interactAlternateButtonIcon;

		[SerializeField] private List<UIAction> _uiActions;
		public void EnableInteractButton(bool enable)
		{
			_interactButton.gameObject.SetActive(enable);
		}

		public void EnableInteractAlternateButton(bool enable, UIActionType type)
		{
			_interactAlternateButton.gameObject.SetActive(enable);
			var sprite = _uiActions.FirstOrDefault(t => t.Type == type)?.Icon;
			_interactAlternateButtonIcon.sprite = sprite;
		}
	}
}