using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public interface IGrabable
{
    public IEnumerator Grab(Rig grabRig, float grabSpeed, Transform currentObject, Transform handTransform);
}
