using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drland.Cook
{
    public class KitchenObjectDataRigging : MonoBehaviour
    {
        [SerializeField] private KitchenObjectType _type;
        [SerializeField] private Transform _leftHandTargetTransform;
        [SerializeField] private Transform _rightHandTargetTransform;

        public Transform GetLeftHandTarget()
        {
            return _leftHandTargetTransform;
        }

        public KitchenObjectType GetKitchenObjectType()
        {
            return _type;
        }
        
        public Transform GetRightHandTarget()
        {
            return _rightHandTargetTransform;
        }
    }
    
    [System.Serializable]
    public class KitchenObjectHolder
    {
        public KitchenObjectType Type;
        public Transform Holder;
    }
}
