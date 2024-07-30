using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingState : CombatState
{
    public AimingState(PlayerStateContext ctx, PlayerCombatStateMachine.ECombatState eCombatState) : base(ctx, eCombatState)
    {
        PlayerStateContext Ctx = ctx;
    }

    public override void EnterState()
    {
        Debug.Log("Entrou No Estado Aiming");
    }
    public override void UpdateState()
    {
        Debug.Log("Esta No Estado Aiming");
    }
    public override void ExitState()
    {
        Debug.Log("Saiu do estado Aiming");
    }

    public override PlayerCombatStateMachine.ECombatState GetNextState()
    {
        if(!Ctx.GetInputHandler.aimTriggered)
        {
            return PlayerCombatStateMachine.ECombatState.Standard;
        }
        return PlayerCombatStateMachine.ECombatState.Aiming;
    }

    public override void OnTriggerEnter(Collider other)
    {

    }

    public override void OnTriggerStay(Collider other)
    {

    }

    public override void OnTriggerExit(Collider other)
    {

    }
}
