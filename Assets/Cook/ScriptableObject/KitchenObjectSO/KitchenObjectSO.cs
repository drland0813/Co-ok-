using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	[CreateAssetMenu()]
	public class KitchenObjectSO : ScriptableObject
	{
		public Transform Prefab;
		public Sprite Sprite;
		public KitchenObjectType Type;
	}

	public enum KitchenObjectType
	{
		Tomato,
		TomatoSlices,
		Cheese,
		CheeseSlice,
		Cabbage,
		CabbageSlices,
		MeatUncooked,
		MeatCooked,
		MeatBurned,
		Bread
	}
}
