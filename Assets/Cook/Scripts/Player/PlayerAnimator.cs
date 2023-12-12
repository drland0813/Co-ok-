using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	public class PlayerAnimator : MonoBehaviour
	{
		[SerializeField] PlayerController _controller;

		private const string IS_WALKING = "isWalking";
		private Animator _animator;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		private void Update()
		{
			_animator.SetBool(IS_WALKING, _controller.IsWalking());
		}
	}
}
