using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	[CreateAssetMenu()]
	public class BurningRecipeSO : ScriptableObject
	{
		public KitchenObjectSO Input;
		public KitchenObjectSO Output;
		public float BurningTimeMax;
	}
}

