using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public interface IPlateHolder
	{
		public event EventHandler OnPlateSpawned;
		public event EventHandler OnPlateRemoved; 
	}
}
