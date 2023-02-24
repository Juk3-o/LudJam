using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectUpgrade : PlayerAbilityState
{
    public CollectUpgrade(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aniBoolName) : base(player, stateMachine, playerData, aniBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;

    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Movement?.SetVelocityZero();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
