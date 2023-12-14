using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public class TrashCounter : BaseCounter
	{
		public override void Interact(PlayerController player)
		{
			if (!player.HasKitchenObject()) return;

			player.GetKitchenObject().DestroySelf();
			SoundManager.Instance.PlaySound(SoundType.Trash, transform);
		}
	}
}