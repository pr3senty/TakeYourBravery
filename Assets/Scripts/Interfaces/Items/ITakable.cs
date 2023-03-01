using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public interface ITakable
{
    public void OnTake(GameObject gameObject);
    
    public IEnumerator Take(Rig takingRig, float takingSpeed, GameObject gameObject, Transform handTransform);

    public AudioSource TakeSound { set; }
}
