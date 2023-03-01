public class IdleState : State
{
    private PlayerController _player;
    
    public IdleState(PlayerController player, StateMachine stateMachine) : base(stateMachine)
    {
        _player = player;
    }

    public override void LogicUpdate()
    {
        if (_player.IsMoving||_player.IsRunning) StateMachine.ChangeState(_player.RunningState);
    }
}
