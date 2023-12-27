using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Drland.Cook
{
    public class SinkCounterVisual : PlatesCounterVisualBase
    {
        [SerializeField] private Transform _washingPoint;
        [SerializeField] private Transform _dirtyPlateVisualPrefab;

        private List<GameObject> _dirtyPlateVisualObjectList;

        private new void Awake()
        {
            base.Awake();
            _dirtyPlateVisualObjectList = new List<GameObject>();
        }

        private new void Start()
        {
            base.Start();
            var sinkCounter = _platesCounter as SinkCounter;
            if (sinkCounter != null) sinkCounter.OnDropDirtyPlates += OnDropDirtyPlates;
            
            TestDirtyPlates();
        }

        private void TestDirtyPlates()
        {
            for (int i = 0; i < 4; i++)
            {
                var plateTransform = Instantiate(_dirtyPlateVisualPrefab, _washingPoint);

                var offset = 0.1f;
                plateTransform.localPosition = new Vector3(offset * _dirtyPlateVisualObjectList.Count, offset * _dirtyPlateVisualObjectList.Count, 0);

                _dirtyPlateVisualObjectList.Add(plateTransform.gameObject);
            }
        }

        private void OnDropDirtyPlates(object sender, SinkCounter.OnDropDirtyPlatesAgrs e)
        {
            for (var i = 0; i < e.TotalPlate; i++)
            {
                var dirtyPlateTransform = Instantiate(_dirtyPlateVisualPrefab, _washingPoint);

                var plateOffsetY = 0.1f;
                dirtyPlateTransform.localPosition = new Vector3(0, plateOffsetY * _plateVisualObjectList.Count, 0);

                _dirtyPlateVisualObjectList.Add(dirtyPlateTransform.gameObject);
            }
        }

        protected override void OnPlateSpawned(object sender, EventArgs e)
        {
            if (_dirtyPlateVisualObjectList.Count > 0)
            {
                var dirtyPlateOnTop = _dirtyPlateVisualObjectList[^1];
                _dirtyPlateVisualObjectList.Remove(dirtyPlateOnTop);
                Destroy(dirtyPlateOnTop);
            }
            
            var plateTransform = Instantiate(_plateVisualPrefab, _counterTopPoint);
            var plateOffsetY = 0.1f;
            plateTransform.localPosition = new Vector3(0, plateOffsetY * _plateVisualObjectList.Count, 0);

            _plateVisualObjectList.Add(plateTransform.gameObject);
        }

        protected override void OnPlateRemoved(object sender, EventArgs eventArgs)
        {
            GameObject plateOnTop = _plateVisualObjectList[^1];
            _plateVisualObjectList.Remove(plateOnTop);
            Destroy(plateOnTop);
        }
    }

}