using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	[CreateAssetMenu()]
	public class RecipeSO : ScriptableObject
	{
		public List<KitchenObjectSO> KitchenObjectSOList;
		public string Name;
	}
}
