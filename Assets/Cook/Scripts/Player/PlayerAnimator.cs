using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	[RequireComponent(typeof(PlayerController))]
	public class PlayerAnimator : MonoBehaviour
	{
		[SerializeField] private PlayerController _controller;

		private const string IS_WALKING = "isWalking";
		private Animator _animator;
		private static readonly int IsWalking = Animator.StringToHash(IS_WALKING);

		public PlayerAnimator(PlayerController controller)
		{
			_controller = controller;
		}

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		private void Update()
		{
			_animator.SetBool(IsWalking, _controller.IsWalking());
		}
	}
}
