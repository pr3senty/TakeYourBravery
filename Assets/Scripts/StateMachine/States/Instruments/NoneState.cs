using UnityEngine;

public class NoneState : InstrumentState
{
    public NoneState(InstrumentsController instrumentsController, StateMachine stateMachine) : base(instrumentsController, stateMachine) {}

    public override void Enter()
    {
        Debug.Log("Enter into NoneState");
        _instrumentsController.CrowBarAction = _instrumentsController.NeedCross;
        _instrumentsController.PipeWrenchAction = _instrumentsController.NeedCross;
        _instrumentsController.SledgeHammerAction = _instrumentsController.NeedCross;
        _instrumentsController.Parent = _instrumentsController.NoneTransform;
        
        _instrumentsController.CanTakeInstrument(_instrumentsController.CrowBarSO, false);
        _instrumentsController.CanTakeInstrument(_instrumentsController.SledgeHammerSO,false);
        _instrumentsController.CanTakeInstrument(_instrumentsController.PipeWrenchSO, false);
        _instrumentsController.CanTakeInstrument(_instrumentsController.CrossSO, true);
    }

    public override void LogicUpdate()
    {

        if (_instrumentsController.ConvertingTo == _instrumentsController.CrossState)
        {
            StateMachine.ChangeState(_instrumentsController.CrossState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _instrumentsController.CanTakeInstrument(_instrumentsController.CrowBarSO, true);
        _instrumentsController.CanTakeInstrument(_instrumentsController.SledgeHammerSO,true);
        _instrumentsController.CanTakeInstrument(_instrumentsController.PipeWrenchSO, true);
    }
}
