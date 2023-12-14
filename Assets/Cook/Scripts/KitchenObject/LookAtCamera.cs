using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public class LookAtCamera : MonoBehaviour
	{
		private enum Mode
		{
			LootAt,
			LookAtInverted,
			CameraForward,
			CameraForwardInverted
		}

		[SerializeField] private Mode _mode;
		private Transform _mainCamera;

		private void Awake()
		{
			if (Camera.main != null) _mainCamera = Camera.main.transform;
		}

		private void LateUpdate()
		{
			switch (_mode)
			{
				case Mode.LootAt:
					transform.LookAt(_mainCamera.transform);
					break;
				case Mode.LookAtInverted:
					var currentTransform = transform.position;
					var dirFromCamera = currentTransform - _mainCamera.position;
					transform.LookAt(currentTransform + dirFromCamera);
					break;
				case Mode.CameraForward:
					transform.forward = _mainCamera.forward;
					break;
				case Mode.CameraForwardInverted:
					transform.forward = -_mainCamera.forward;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
