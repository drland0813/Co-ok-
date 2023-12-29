using System.Collections;
using System.Collections.Generic;
using Drland.Cook;
using UnityEngine;

public class DirtyPlateStackKitchenObject : KitchenObject
{
    [SerializeField] private Transform _dirtyPlateVisual;
    [SerializeField] private Transform _dirtyPlateHolder;
    [SerializeField] private float _offSetY = 0.1f;

    private int _quantity;

    public int Quantity => _quantity;

    public void InitStack(int quantity)
    {
        _quantity = quantity;
        for (var i = 0; i < quantity; i++)
        {
            var dirtyPlate = Instantiate(_dirtyPlateVisual, _dirtyPlateHolder);
            var newPos = new Vector3(0, _offSetY * (i + 1), 0);
            dirtyPlate.transform.localPosition = newPos;
            dirtyPlate.gameObject.SetActive(true);
        }
    }
}
