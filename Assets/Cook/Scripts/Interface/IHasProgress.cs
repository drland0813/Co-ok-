using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public interface IHasProgress
	{
		public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
		public class OnProgressChangedEventArgs : EventArgs
		{
			public float ProgessNomarlized;
		}
	}
}
