using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	public class TrashCounter : BaseCounter
	{
		public override void Interact(PlayerController player)
		{
			if (player.HasKitchenObject())
			{
				player.GetKitchenObject().DestroySelf();

				SoundManager.Instance.PlaySound(SoundType.Trash, transform);
			}
		}
	}
}