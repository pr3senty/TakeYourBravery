using UnityEngine;

public class PipeWrenchState : InstrumentState
{
    public PipeWrenchState(InstrumentsController instrumentsController, StateMachine stateMachine) : base(instrumentsController, stateMachine) {}

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Converting into PipeWrench");
        _instrumentsController.PipeWrenchAction = gameObject => _instrumentsController.BreakLock(_instrumentsController.PipeWrenchSO, gameObject);
        _instrumentsController.Parent = _instrumentsController.PipeWenchTransform;
        
        _instrumentsController.CanTakeInstrument(_instrumentsController.PipeWrenchSO,false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_instrumentsController.IsUsed) StateMachine.ChangeState(_instrumentsController.NoneState);
    }

    public override void Exit()
    {
        base.Exit();
        _instrumentsController.CanTakeInstrument(_instrumentsController.PipeWrenchSO,true);
        _instrumentsController.ResetInstrument(_instrumentsController.PipeWrenchSO);
    }
}
