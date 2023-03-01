using System;
using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(AudioSource))]
public class Takable : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private BoolEventSO _onItemLook;
    [SerializeField] private ScriptableObject _takableItem;
    
    [Header("Animation Rigging")]
    [SerializeField] private Rig _takingRig;
    [SerializeField] private Transform _armBeginTransform;
    [SerializeField] private Transform _handTransform;
    [SerializeField] private Transform _takingItem;
    [SerializeField] private float _takingSpeed;

    private bool _isTaking;
    private ITakable _item;
    private float _distanceBetweenObjectAndHand;
    private float _armLength;

    private void OnEnable()
    {
        try
        {
            _item = (ITakable)_takableItem;
        }
        catch (InvalidCastException)
        {
            throw new NullReferenceException("Takable item must be ITakable");
        }
        
        _inputHandler.OnIntractEvent += OnTake;
        _item.TakeSound = GetComponent<AudioSource>();
        
        _distanceBetweenObjectAndHand = Math.Abs(transform.position.x - _handTransform.position.x);
        _armLength = Math.Abs(_armBeginTransform.position.y - _handTransform.position.y) + 0.1f;
    }

    public void OnTake()
    {
        if (!_isTaking && _distanceBetweenObjectAndHand < _armLength)
        {   
            _item.OnTake(gameObject);
            _isTaking = true;

            _takingItem.position = transform.position;
            StartCoroutine(Take());
        }
    }

    private void Update()
    {
        if (!_isTaking && _distanceBetweenObjectAndHand < _armLength) _onItemLook.Action.Invoke(true);
    }

    private IEnumerator Take()
    {
        yield return StartCoroutine(_item.Take(_takingRig, _takingSpeed, gameObject, _handTransform));
        
        _isTaking = false;
    }
    
    private void OnDisable()
    {
        _onItemLook.Action.Invoke(false);
        _isTaking = false;
        _inputHandler.OnIntractEvent -= OnTake;
    }
}
