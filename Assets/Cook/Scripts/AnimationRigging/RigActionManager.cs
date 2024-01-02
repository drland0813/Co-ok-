using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Drland.Cook
{
    public class RigActionManager : MonoBehaviour
    {
        [SerializeField] private RigBuilder _rigBuilder;
        [SerializeField] private RigHoldAction _holdAction;
        [SerializeField] private RigWashAction _washAction;

        public void Enable(PlayerActionType actionType, bool enable)
        {
            switch (actionType)
            {
                case PlayerActionType.Hold:
                    _rigBuilder.enabled = false;
                    _holdAction.Enable(enable);
                    _rigBuilder.enabled = true;
                    break;
                case PlayerActionType.Wash:
                    _washAction.Enable(enable);
                    break;
                case PlayerActionType.Cut:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null);
            }
        }
    }
}
