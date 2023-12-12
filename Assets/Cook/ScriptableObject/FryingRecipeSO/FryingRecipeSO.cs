using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	[CreateAssetMenu()]
	public class FryingRecipeSO : ScriptableObject
	{
		public KitchenObjectSO Input;
		public KitchenObjectSO Output;
		public float FryingTimeMax;
	}
}
