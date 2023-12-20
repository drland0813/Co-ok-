using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	[RequireComponent(typeof(PlayerController))]
	public class PlayerAnimator : MonoBehaviour
	{
		private const string IS_WALKING = "IsWalking";
		private const int HANDLE_OBJECT_LAYER = 1;
		private static readonly int IsWalking = Animator.StringToHash(IS_WALKING);
		
		[SerializeField] private Animator _animator;

		private PlayerController _controller;

		private void Awake()
		{
			_controller = GetComponent<PlayerController>();
		}

		private void Update()
		{
			_animator.SetBool(IsWalking, _controller.IsWalking());
			_animator.SetLayerWeight(HANDLE_OBJECT_LAYER, _controller.IsHavingKitchenObject());
		}
	}
}
