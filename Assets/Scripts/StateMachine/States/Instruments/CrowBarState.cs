using UnityEngine;

public class CrowBarState : InstrumentState
{
    public CrowBarState(InstrumentsController instrumentsController, StateMachine stateMachine) : base(
        instrumentsController, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Converting into CrowBar");
        _instrumentsController.CrowBarAction = gameObject => _instrumentsController.RemoveBarricade(_instrumentsController.CrowBarSO, gameObject);
        _instrumentsController.Parent = _instrumentsController.CrowBarTransform;

        _instrumentsController.CanTakeInstrument(_instrumentsController.CrowBarSO,false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_instrumentsController.IsUsed) StateMachine.ChangeState(_instrumentsController.NoneState);
    }

    public override void Exit()
    {
        base.Exit();
        _instrumentsController.CanTakeInstrument(_instrumentsController.CrowBarSO,true);
        _instrumentsController.ResetInstrument(_instrumentsController.CrowBarSO);
    }
}
