using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cook
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
