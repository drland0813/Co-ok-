using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public class CuttingCounterVisual : MonoBehaviour
	{
		private const string CUT = "Cut";
		private static readonly int Cut = Animator.StringToHash(CUT);


		[SerializeField] private CuttingCounter _cuttingCounter;

		private Animator _animator;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		private void Start()
		{
			_cuttingCounter.OnCut += CuttingCounter_OnCut;
		}

		private void CuttingCounter_OnCut(object sender, EventArgs e)
		{
			_animator.SetTrigger(Cut);
		}
	}

}