using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Drland.Cook
{
	[CreateAssetMenu()]
	public class KitchenObjectIconsData : ScriptableObject
	{
		public List<KitchenObject_Icon> Data;

		public Sprite GetKitchenObjectIcon(KitchenObjectSO kitchenObjectSO)
		{
			KitchenObject_Icon iconKitchenObjet = Data.FirstOrDefault(k => k.Type == kitchenObjectSO.Type);
			if (EqualityComparer<KitchenObject_Icon>.Default.Equals(iconKitchenObjet, default))
			{
				return iconKitchenObjet.Sprite;
			}
			else
			{
				return null;
			}
		}
	}


	[System.Serializable]
	public struct KitchenObject_Icon
	{
		public KitchenObjectType Type;
		public Sprite Sprite;
	}
}