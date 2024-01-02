using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Drland.Cook
{
    public class RigHoldAction : RigActionBase
    {
        [SerializeField] private TwoBoneIKConstraint _leftHand;
        [SerializeField] private TwoBoneIKConstraint _rightHand;

        private KitchenObjectDataRigging _dataRigging;

        public override void Enable(bool enable)
        {
            if (enable)
            {
                _dataRigging = PlayerController.Instance.GetKitchenObject().GetKitchenObjectDataRigging();
                _leftHand.data.target = _dataRigging.GetLeftHandTarget();
                _rightHand.data.target = _dataRigging.GetRightHandTarget();
                _rig.weight = 1;
            }
            else
            {
                _dataRigging = null;
                _rig.weight = 0;
                _leftHand.data.target = null;
                _rightHand.data.target = null;
            }
        }
    }
}
