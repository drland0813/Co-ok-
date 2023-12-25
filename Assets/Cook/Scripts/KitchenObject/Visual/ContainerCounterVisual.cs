using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public class ContainerCounterVisual : MonoBehaviour
	{
		private const string OPEN_ClOSE = "OpenClose";

		[SerializeField] private ContainerCounter _containerCounter;

		private Animator _animator;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		private void Start()
		{
			_containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
		}

		private void ContainerCounter_OnPlayerGrabbedObject(object sender, EventArgs e)
		{
			_animator.SetTrigger(OPEN_ClOSE);
		}
	}

}