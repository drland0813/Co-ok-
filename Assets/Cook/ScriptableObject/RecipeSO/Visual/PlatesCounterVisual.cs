using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
	public class PlatesCounterVisual : MonoBehaviour
	{
		[SerializeField] private Transform _counterTopPoint;
		[SerializeField] private Transform _plateVisualPrefab;

		[SerializeField] private PlatesCounter _platesCounter;

		private List<GameObject> _plateVisualObjectList;

		private void Awake()
		{
			_plateVisualObjectList = new();
		}

		private void Start()
		{
			_platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
			_platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
		}

		private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
		{
			GameObject plateOnTop = _plateVisualObjectList[_plateVisualObjectList.Count - 1];
			_plateVisualObjectList.Remove(plateOnTop);
			Destroy(plateOnTop);
		}

		private void PlatesCounter_OnPlateSpawned(object sender, EventArgs e)
		{
			Transform plateTransform = Instantiate(_plateVisualPrefab, _counterTopPoint);

			float plateOffsetY = 0.1f;
			plateTransform.localPosition = new Vector3(0, plateOffsetY * _plateVisualObjectList.Count, 0);

			_plateVisualObjectList.Add(plateTransform.gameObject);
		}
	}
}
