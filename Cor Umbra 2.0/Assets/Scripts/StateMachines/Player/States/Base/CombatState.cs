using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatState : BaseState<PlayerCombatStateMachine.ECombatState>
{
    protected PlayerStateContext Ctx;

    public CombatState(PlayerStateContext ctx, PlayerCombatStateMachine.ECombatState stateKey) : base(stateKey)
    {
        Ctx = ctx;
    }
}
