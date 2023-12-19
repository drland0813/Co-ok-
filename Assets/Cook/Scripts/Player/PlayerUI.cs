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


		private Action _onInteractObject;
		private Action _onInteractAlternateObject;


		private void Awake()
		{
			_interactButton.onClick.AddListener(() =>
			{
				_onInteractObject?.Invoke();
			});

			_interactAlternateButton.onClick.AddListener(() =>
			{
				_onInteractAlternateObject?.Invoke();
			});
		}

		public void RegisterInteractObjectCallback(Action interactObjectAction)
		{
			_onInteractObject = interactObjectAction;
		}

		public void RegisterInteractAlternateObjectCallback(Action interactAlternateObjectAction)
		{
			_onInteractAlternateObject = interactAlternateObjectAction;
		}

		public void EnableInteractAlternateButton(bool enable)
		{
			_interactAlternateButton.gameObject.SetActive(enable);
		}
	}

}