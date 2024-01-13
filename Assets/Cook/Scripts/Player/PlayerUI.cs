using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Drland.Cook
{
	public enum PlayerActionType
	{
		Hold,
		Cut,
		Wash,
	}

	[Serializable]
	public class UIAction
	{
		public PlayerActionType Type;
		public Sprite Icon;
	}
	public class PlayerUI : MonoBehaviour
	{
		[SerializeField] private Button _interactButton;
		[SerializeField] private Button _interactAlternateButton;
		[SerializeField] private Image _interactAlternateButtonIcon;

		[SerializeField] private List<UIAction> _uiActions;

		private Dictionary<PlayerActionType, Sprite> _spritesActionCache;

		private void Awake()
		{
			_spritesActionCache = new Dictionary<PlayerActionType, Sprite>();
			foreach (var uiAction in _uiActions)
			{
				_spritesActionCache.Add(uiAction.Type, uiAction.Icon);
			}
		}

		public void EnableInteractAlternateButton(bool enable, PlayerActionType type)
		{
			_interactAlternateButton.gameObject.SetActive(enable);
			if (!enable) return;

			var sprite = _spritesActionCache[type];
			_interactAlternateButtonIcon.sprite = sprite;
		}
	}
}