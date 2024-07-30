using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveState : BaseState<MovementPlayerStateMachine.EMovementState>
{
    protected PlayerStateContext Ctx;

    public MoveState(PlayerStateContext ctx, MovementPlayerStateMachine.EMovementState stateKey) : base(stateKey)
    {
        Ctx = ctx;
    }

    
}
