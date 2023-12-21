using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Drland.Cook
{
	[CreateAssetMenu()]
	public class KitchenObjectSO : ScriptableObject
	{
		public Transform Prefab;
		public Sprite Sprite;
		public bool IsIngredient;
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
