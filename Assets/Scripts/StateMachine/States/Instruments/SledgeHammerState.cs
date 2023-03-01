using UnityEngine;

public class SledgeHammerState : InstrumentState
{
    public SledgeHammerState(InstrumentsController instrumentsController, StateMachine stateMachine) : base(instrumentsController, stateMachine) {}

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Converting into SledgeHammer");
        _instrumentsController.SledgeHammerAction = gameObject => _instrumentsController.BreakWall(_instrumentsController.SledgeHammerSO, gameObject);
        _instrumentsController.Parent = _instrumentsController.SledgeHammerTransform;
        
        _instrumentsController.CanTakeInstrument(_instrumentsController.SledgeHammerSO,false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_instrumentsController.IsUsed) StateMachine.ChangeState(_instrumentsController.NoneState);
    }

    public override void Exit()
    {
        base.Exit();
        _instrumentsController.CanTakeInstrument(_instrumentsController.SledgeHammerSO, true);
        _instrumentsController.ResetInstrument(_instrumentsController.SledgeHammerSO);
    }
}
