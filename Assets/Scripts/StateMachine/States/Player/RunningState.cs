public class RunningState : State
{
    private PlayerController _player;
    
    private float _runSpeed;
    private float _walkSpeed;
    private float _rotationSpeed;


    public RunningState(PlayerController player, StateMachine stateMachine) : base(stateMachine)
    {
        _player = player;
    }

    public override void Enter()
    {
        base.Enter();
        _runSpeed = _player.RunSpeed;
        _walkSpeed = _player.WalkSpeed;
    }

    public override void PhysicsUpdate()
    {
        if (_player.IsRunning) { _player.Move(_runSpeed); }
        else { _player.Move(_walkSpeed); }
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!_player.IsMoving) StateMachine.ChangeState(_player.IdleState);
    }

    public override void Exit()
    {
        _player.ResetMoveParams();
    }
}
