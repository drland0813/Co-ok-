using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Drland.Cook
{
    public abstract class RigActionBase : MonoBehaviour
    {
        [SerializeField] protected PlayerActionType _type;
        [SerializeField] protected Rig _rig;

        public abstract void Enable(bool enable);
    }
}
