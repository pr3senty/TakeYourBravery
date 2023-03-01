public abstract class InstrumentState : State
{
    protected readonly InstrumentsController _instrumentsController;

    protected InstrumentState(InstrumentsController instrumentsController, StateMachine stateMachine) : base(
        stateMachine)
    {
        _instrumentsController = instrumentsController;
    }

    public override void Enter() => _instrumentsController.IsUsed = false;

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_instrumentsController.ConvertingTo != StateMachine.CurrentState)
        {
            switch (_instrumentsController.ConvertingTo)
            {
                case (CrowBarState):
                    StateMachine.ChangeState(_instrumentsController.CrowBarState);
                    _instrumentsController.ConvertingTo = _instrumentsController.NoneState;
                    break;
                case (PipeWrenchState):
                    StateMachine.ChangeState(_instrumentsController.PipeWrenchState);
                    _instrumentsController.ConvertingTo = _instrumentsController.NoneState;
                    break;
                case (SledgeHammerState):
                    StateMachine.ChangeState(_instrumentsController.SledgeHammerState);
                    _instrumentsController.ConvertingTo = _instrumentsController.NoneState;
                    break;
            }
        }
    }
}
