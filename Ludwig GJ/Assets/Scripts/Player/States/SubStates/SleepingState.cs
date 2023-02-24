using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingState : PlayerAbilityState
{
    public SleepingState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aniBoolName) : base(player, stateMachine, playerData, aniBoolName)
    {
    }

    private bool canWake;
    private float waitTime = 4f;

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.Anim.SetBool("getUp", false);
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

        player.Sleep();

        player.InputHandler.CanUseInput = false;
        canWake = true;

    }

    public override void Exit()
    {
        base.Exit();

        player.InputHandler.CanUseInput = true;

        player.Music();

        TimerControl.Instance.BeginTimer();


    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.Movement?.SetVelocityZero();

        if (player.InputHandler.GetUp && Time.time > startTime + waitTime && canWake)
        {
            player.Anim.SetBool("getUp", true);
            player.StopSleep();
            player.Wake();
            canWake = false;
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
