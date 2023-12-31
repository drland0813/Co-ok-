using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public abstract class PlatesCounterVisualBase : MonoBehaviour
	{
		[SerializeField] protected Transform _counterTopPoint;
		[SerializeField] protected Transform _plateVisualPrefab;
		[SerializeField] protected PlateHolderCounterBase _platesCounter;
		
		protected List<GameObject> _plateVisualObjectList;
		protected void Awake()
		{
			_plateVisualObjectList = new List<GameObject>();
		}
		
		protected virtual void OnEnable()
		{
			_platesCounter.OnPlateSpawned += OnPlateSpawned;
			_platesCounter.OnPlateRemoved += OnPlateRemoved;
		}

		protected abstract void OnPlateSpawned(object sender, EventArgs eventArgs);
		protected abstract void OnPlateRemoved(object sender, EventArgs eventArgs);

	}
	public class PlatesCounterVisual : PlatesCounterVisualBase
	{
		protected override void OnPlateRemoved(object sender, EventArgs e)
		{
			if (_plateVisualObjectList.Count == 0) return;

			var plateOnTop = _plateVisualObjectList[^1];
			_plateVisualObjectList.Remove(plateOnTop);
			Destroy(plateOnTop);
		}

		protected override void OnPlateSpawned(object sender, EventArgs e)
		{
			var plateTransform = Instantiate(_plateVisualPrefab, _counterTopPoint);

			var plateOffsetY = 0.1f;
			plateTransform.localPosition = new Vector3(0, plateOffsetY * _plateVisualObjectList.Count, 0);
			_plateVisualObjectList.Add(plateTransform.gameObject);
		}
	}
}
