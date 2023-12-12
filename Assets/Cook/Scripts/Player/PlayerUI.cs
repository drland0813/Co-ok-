using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cook
{
	public class PlayerUI : MonoBehaviour
	{
		[SerializeField] Button _interactButton;
		[SerializeField] Button _interactAlternatButton;


		private Action _onInteractObject;
		private Action _onInteractAlternateObject;


		private void Awake()
		{
			_interactButton.onClick.AddListener(() =>
			{
				_onInteractObject?.Invoke();
			});

			_interactAlternatButton.onClick.AddListener(() =>
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
			_interactAlternatButton.gameObject.SetActive(enable);
		}
	}

}