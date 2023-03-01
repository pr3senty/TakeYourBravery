
using UnityEngine;

public class CrossState : InstrumentState
{

    public CrossState(InstrumentsController instrumentsController, StateMachine stateMachine) : base(
        instrumentsController, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Converting into cross");
        _instrumentsController.CrowBarAction = _instrumentsController.CantDoAnything;
        _instrumentsController.PipeWrenchAction = _instrumentsController.CantDoAnything;
        _instrumentsController.SledgeHammerAction = _instrumentsController.CantDoAnything;
        _instrumentsController.Parent = _instrumentsController.CrossTransform;

        _instrumentsController.CanTakeInstrument(_instrumentsController.CrossSO,false);

    }

    public override void Exit()
    {
        base.Exit();
        _instrumentsController.CanTakeInstrument(_instrumentsController.CrossSO,true);
        _instrumentsController.ResetInstrument(_instrumentsController.CrossSO);
    }
}
