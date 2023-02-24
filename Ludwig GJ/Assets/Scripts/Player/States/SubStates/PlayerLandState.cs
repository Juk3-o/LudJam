using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aniBoolName) : base(player, stateMachine, playerData, aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Height.GetComponentInChildren<Animator>().SetTrigger("SLand");

        player.Land();

        player.ParticleManager?.StartParticles(player.DustLandParticles);

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);

            }
            else if (isAnimationFinished)
            {
                player.Movement?.SetVelocityZero();
                stateMachine.ChangeState(player.IdleState);

            }
        }
    }

}
