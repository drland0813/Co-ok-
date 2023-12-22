using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Drland.Cook
{
    public enum ArmRiggingState
    {
        Idle = 0,
        Handle = 1
    }
    public class ArmsRiggingController : MonoBehaviour
    {
        [SerializeField] private TwoBoneIKConstraint _leftHand;
        [SerializeField] private TwoBoneIKConstraint _rightHand;

        private Rig _rig;

        private void Awake()
        {
            _rig = GetComponent<Rig>();
        }

        public void EnableArmsRigging(KitchenObjectDataRigging kitchenObjectDataRigging)
        {
            _leftHand.data.target = kitchenObjectDataRigging.GetLeftHandTarget();
            _rightHand.data.target = kitchenObjectDataRigging.GetRightHandTarget();
            _rig.weight = 1;
        }
        
        public void DisableArmsRigging()
        {
            _rig.weight = 0;
            _leftHand.data.target = null;
            _rightHand.data.target = null;
        }
    }
}
