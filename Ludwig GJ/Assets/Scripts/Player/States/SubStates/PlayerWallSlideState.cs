using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aniBoolName) : base(player, stateMachine, playerData, aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.Height.GetComponentInChildren<Animator>().SetTrigger("SWallLand");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            Movement?.SetVelocityY(-playerData.wallSlideVelocity);

        }
    }
}
