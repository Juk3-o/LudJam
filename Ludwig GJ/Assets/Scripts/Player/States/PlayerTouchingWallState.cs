using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aniBoolName) : base(player, stateMachine, playerData, aniBoolName)
    {
    }

    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool isTouchingledge;
    protected int xInput;
    protected bool jumpInput;
    protected bool dashInput;

    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    protected CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        if (CollisionSenses)
        {

            isGrounded = CollisionSenses.Ground;
            isTouchingWall = CollisionSenses.WallFront;
            isTouchingledge = CollisionSenses.LedgeHorizontal;
        }


        player.DashState.ResetCanDash();


        if (isTouchingWall && !isTouchingledge)
        {
            player.LedgeClimbState.SetDetectedPostion(player.transform.position);
        }
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

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        dashInput = player.InputHandler.DashInput;

        if (jumpInput && playerData.WallClimbAbility)
        {

            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash() && playerData.DashAbility && playerData.WallClimbAbility)
        {

            player.DashState.DetermineDashDirection(isTouchingWall);
            stateMachine.ChangeState(player.DashState);
        }
        else if (isGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (!isTouchingWall || xInput != Movement?.FacingDirection)
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if (isTouchingWall && !isTouchingledge)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
