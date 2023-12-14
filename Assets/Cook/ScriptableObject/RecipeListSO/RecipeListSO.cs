using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	[CreateAssetMenu()]
	public class RecipeListSO : ScriptableObject
	{
		public List<RecipeSO> Recipes;
	}
}