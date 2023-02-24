using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    private bool isTouchingWall;
    private bool isGrounded;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aniBoolName) : base(player, stateMachine, playerData, aniBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        player.JumpState.resetAmountOfJumpsLeft();

        player.Height.GetComponentInChildren<Animator>().SetTrigger("SJump");

        Movement?.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);

        Movement?.CheckIfShouldFlip(wallJumpDirection);
        player.JumpState.decreaseAmountOfJumpsLeft();

        player.Jump();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isTouchingWall = CollisionSenses.WallFront;
        isGrounded = CollisionSenses.Ground;
        xInput = player.InputHandler.NormInputX;

        player.Anim.SetFloat("yVelocity", Movement.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));

        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }

        if (isTouchingWall && xInput == Movement?.FacingDirection && Movement?.CurrentVelocity.y < 0)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }

    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -Movement.FacingDirection;
            

        }
        else
        {
            wallJumpDirection = Movement.FacingDirection;
        }
    }
}
