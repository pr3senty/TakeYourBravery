using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private AudioSource _stepSource;
    [SerializeField] private float _walkSpeed = 7f;
    [SerializeField] private float _runSpeed = 10f; 
    [SerializeField] private float _rotationSpeed = 2f;
    
    private Animator _anim;
    private StateMachine _stateMachine;
    private Vector2 _inputVector;
    private Rigidbody _rb;
    private bool _isRunning;
    private bool _isMoving;

    public RunningState RunningState { get; set; }
    public IdleState IdleState { get; set; }

    public float WalkSpeed { get => _walkSpeed; }
    public float RunSpeed { get => _runSpeed; }
    public float RotationSpeed { get => _rotationSpeed; }
    public bool IsRunning { get => _isRunning; }

    public bool IsMoving { get => _isMoving; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _anim = GetComponent<Animator>();
        _stateMachine = new StateMachine();

        RunningState = new RunningState(this, _stateMachine);
        IdleState = new IdleState(this, _stateMachine);
        
        _stateMachine.Initialize(RunningState);
    }

    private void Update()
    {
        _stateMachine.OnUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine.OnPhysicsUpdate();
    }

    private void OnEnable()
    {
        _inputHandler.MoveEvent += OnMove;
        _inputHandler.OnRunEvent += OnRun;
        _inputHandler.StopRunEvent += StopRun;
        _inputHandler.StopMoveEvent += StopMove;
    }

    public void Move(float speed)
    {
        Vector3 movementVector = transform.forward * _inputVector.y * speed;

        _rb.angularVelocity = _inputVector.x * _rotationSpeed * Vector3.up;
        
        _rb.AddForce(movementVector, ForceMode.Impulse);
        
        
        _anim.SetFloat("AngularSpeed", _rb.angularVelocity.y);
        _anim.SetFloat("Speed", _isRunning ? 2 : 1);
    }
    private void OnMove(Vector2 movement)
    {
        _inputVector = movement;
        
        if (!_stepSource.isPlaying)_stepSource.Play();
        _isMoving = true;
    }

    private void StopMove() { _isMoving = false; _stepSource.Stop();}
    
    public void ResetMoveParams()
    {
        _rb.angularVelocity = Vector3.zero;
        
        _anim.SetFloat("AngularSpeed", 0f);
        _anim.SetFloat("Speed", 0f);
    }

    private void OnRun() { _isRunning = true; }
    private void StopRun() { _isRunning = false; }
}
