using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Serialization;

namespace Drland.Cook
{
    public class RigWashAction : RigActionBase
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private List<GameObject> _objectData;

        public override void Enable(bool enable)
        {
            float weight = enable ? 1 : 0;
            _rig.weight = weight;
            _animator.SetLayerWeight(1, weight);
            foreach (var go in _objectData)
            {
                go.SetActive(enable);
            }
        }
    }
}
