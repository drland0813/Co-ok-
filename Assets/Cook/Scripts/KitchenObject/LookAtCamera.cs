using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	public class LookAtCamera : MonoBehaviour
	{
		public enum Mode
		{
			LootAt,
			LookAtInverted,
			CameraForward,
			CameraForwardInverted
		}

		[SerializeField] private Mode _mode;

		private void LateUpdate()
		{
			switch (_mode)
			{
				case Mode.LootAt:
					transform.LookAt(Camera.main.transform);
					break;
				case Mode.LookAtInverted:
					Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
					transform.LookAt(transform.position + dirFromCamera);
					break;
				case Mode.CameraForward:
					transform.forward = Camera.main.transform.forward;
					break;
				case Mode.CameraForwardInverted:
					transform.forward = -Camera.main.transform.forward;
					break;

			}
		}
	}
}
