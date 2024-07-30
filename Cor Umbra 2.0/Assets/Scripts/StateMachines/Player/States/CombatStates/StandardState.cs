using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardState : CombatState
{
    public StandardState(PlayerStateContext ctx, PlayerCombatStateMachine.ECombatState eCombatState) : base(ctx, eCombatState)
    {
        PlayerStateContext Ctx = ctx;
    }

    public override void EnterState()
    {
        Debug.Log("Entrou No Estado Standard");
    }
    public override void UpdateState()
    {
        Debug.Log("Esta No Estado Standard");
    }
    public override void ExitState()
    {
        Debug.Log("Saiu do estado Standard");
    }

    public override PlayerCombatStateMachine.ECombatState GetNextState()
    {
        if(Ctx.GetInputHandler.aimTriggered)
        {
            return PlayerCombatStateMachine.ECombatState.Aiming;
        }
        return PlayerCombatStateMachine.ECombatState.Standard;
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
