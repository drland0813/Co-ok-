using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
{
	public abstract class UIController : MonoBehaviour
	{
		public abstract void Initialize();

		public virtual void Hide() => gameObject.SetActive(false);
		public virtual void Show() => gameObject.SetActive(true);
	}
}