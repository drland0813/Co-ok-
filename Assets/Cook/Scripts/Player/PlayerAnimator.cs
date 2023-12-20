using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	[RequireComponent(typeof(PlayerController))]
	public class PlayerAnimator : MonoBehaviour
	{
		[SerializeField] private Animator _animator;

		private PlayerController _controller;

		private const string IS_WALKING = "IsWalking";
		private static readonly int IsWalking = Animator.StringToHash(IS_WALKING);

		private void Awake()
		{
			_controller = GetComponent<PlayerController>();
		}

		private void Update()
		{
			_animator.SetBool(IsWalking, _controller.IsWalking());
		}
	}
}
