using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[CreateAssetMenu(fileName = "New Door", menuName = "Scriptable Objects/New Door")]
public class Door : ScriptableObject, IInteractable, IGrabable
{
    [SerializeField] private int _openAngle;

    private bool _isOpen;
    private Transform _rotatingTransform;
    
    public void Interact(GameObject gameObject)
    {
        _rotatingTransform = gameObject.transform.parent.transform;
        
        _rotatingTransform.Rotate(0, _openAngle * (_isOpen ? -1 : 1), 0);
        _isOpen = !_isOpen;
    }
    
    public IEnumerator Grab(Rig grabRig, float grabSpeed, Transform currentObject, Transform handTransform)
    {
        while (grabRig.weight < 1f)
        {
            grabRig.weight = Mathf.MoveTowards(grabRig.weight, 1, grabSpeed * Time.deltaTime);
            yield return null;
        }
        
        while (grabRig.weight > 0f)
        {
            grabRig.weight = Mathf.MoveTowards(grabRig.weight, 0, grabSpeed * Time.deltaTime); ;
            yield return null;
        }
    }
}
