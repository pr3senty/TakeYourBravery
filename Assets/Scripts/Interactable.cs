using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Interactable : MonoBehaviour
{
    [SerializeField] private ScriptableObject _interactableItem;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private BoolEventSO _onItemLook;

    [SerializeField] private Transform _armBeginTransform;
    [SerializeField] private Transform _handTransform;
    [SerializeField] private float _overrideInteractDistance;
    
    [Header("Animation Rigging (Only If object is IGrabable)")]
    [SerializeField] private Rig _grabRig;
    [SerializeField] private Transform _grabingTarget;
    [SerializeField] private float _grabSpeed;

    private IInteractable _item;
    private IGrabable _grabableItem;
    private float _distanceBetweenObjectAndHand;
    private float _interactDistance;

    private void OnEnable()
    {
        if (_item is null)
        {
            try
            {
                _item = (IInteractable)_interactableItem;
            }
            catch (InvalidCastException)
            {
                throw new NullReferenceException("InteractableItem must be IInteractable");
            }

            try
            {
                _grabableItem = (IGrabable)_interactableItem;
            }
            catch (InvalidCastException)
            {
                _grabableItem = null;
            }
        }
        
        _inputHandler.OnIntractEvent += OnInteract;
        
        
        _distanceBetweenObjectAndHand = Mathf.Abs(transform.position.x - _handTransform.position.x);
        _interactDistance = (_overrideInteractDistance == 0 ? Math.Abs(_armBeginTransform.position.y - _handTransform.position.y) : _overrideInteractDistance);
    }

    private void Update()
    {
        if (_distanceBetweenObjectAndHand <= _interactDistance)
        {
            _onItemLook.Action.Invoke(true);
        }
    }

    private void OnDisable()
    {
        _inputHandler.OnIntractEvent -= OnInteract;
        _onItemLook.Action.Invoke(false);
    }

    public void OnInteract()
    {
        if (_distanceBetweenObjectAndHand <= _interactDistance)
        {
            if (_grabableItem is null || _grabRig is null) _item.Interact(gameObject);
            else
            {
                _grabRig.transform.GetChild(0).position = _grabingTarget.position;
                StartCoroutine(Grab(_grabRig, _grabSpeed, transform, _handTransform));
            }
        }
    }

    private IEnumerator Grab(Rig grabRig, float grabSpeed, Transform currentTransform, Transform handTransform)
    {
        yield return StartCoroutine(_grabableItem.Grab(grabRig, grabSpeed, currentTransform, handTransform));
        _item.Interact(gameObject);
    }
}
