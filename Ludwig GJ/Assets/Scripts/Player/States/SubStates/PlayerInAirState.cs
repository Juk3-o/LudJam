using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    protected Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;
    protected CollisionSenses CollisionSenses { get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;

    public static bool canMoveInAir = true;

    //Input
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool dashInput;
    private bool moveInput;

    //Checks
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool isTouchingLedge;
    private bool wallJumpCoyoteTime;

    private bool coyoteTime;
    private bool isJumping;
    private bool isAllowedLedgeClimb;
    private float startWallJumpCoyoteTime;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aniBoolName) : base(player, stateMachine, playerData, aniBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        if (CollisionSenses)
        {
            //Checks
            isGrounded = CollisionSenses.Ground;
            isTouchingWall = CollisionSenses.WallFront;
            isTouchingWallBack = CollisionSenses.WallBack;
            isTouchingLedge = CollisionSenses.LedgeHorizontal;
        }


        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPostion(player.transform.position);
        }

        if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();

        }
    }

    public override void Enter()
    {
        base.Enter();
        isAllowedLedgeClimb = false;
    }

    public override void Exit()
    {
        base.Exit();

        isTouchingWall = false;
        oldIsTouchingWall = false;
        isTouchingWallBack = false;
        oldIsTouchingWallBack = false;

        player.RB.drag = 0;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isJumping)
        {
            player.RB.drag = 1;
        }

        //Debug.Log(isTouchingWall);

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        dashInput = player.InputHandler.DashInput;
        moveInput = player.InputHandler.MoveInput;

        if (Time.time > 0.2 + startTime)
        {
            isAllowedLedgeClimb = true;

        }

        CheckJumpMutiplyer();

        if (isGrounded && Movement?.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandState);
        }
        else if (isTouchingWall && !isTouchingLedge && !isGrounded && isAllowedLedgeClimb)
        {
            stateMachine.ChangeState(player.LedgeClimbState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            coyoteTime = false;

            stateMachine.ChangeState(player.JumpState);
        }
        else if (isTouchingWall && xInput == Movement?.FacingDirection && Movement?.CurrentVelocity.y < 0 && playerData.WallClimbAbility)
        {
            stateMachine.ChangeState(player.WallSlideState);
        }
        else if (jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime) && playerData.WallClimbAbility)
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = CollisionSenses.WallFront;
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.WallJumpState);
        }
        else if (dashInput && player.DashState.CheckIfCanDash() && playerData.DashAbility)
        {
            player.DashState.DetermineDashDirection(false);
            stateMachine.ChangeState(player.DashState);
        }
        else
        {
            Movement?.CheckIfShouldFlip(xInput);

            if (player.RB.bodyType != RigidbodyType2D.Static)
            {
                if (canMoveInAir)
                {
                    Movement?.SetVelocityX(Mathf.MoveTowards(playerData.movementVelocity * xInput , playerData.inAirVelocity * xInput, playerData.rateOfDeceleration));
                }
            }

            player.Anim.SetFloat("yVelocity", Movement.CurrentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));
        }
    }

    private void CheckJumpMutiplyer()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                Movement?.SetVelocityY(Movement.CurrentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (Movement?.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.resetAmountOfJumpsLeft();
            player.JumpState.decreaseAmountOfJumpsLeft();
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.wallJumpCoyoteTime)
        {
            wallJumpCoyoteTime = false;
            player.JumpState.resetAmountOfJumpsLeft();
            player.JumpState.decreaseAmountOfJumpsLeft();
        }
    }

    public void StartCoyoteTime()
    {
        coyoteTime = true;
    }

    public void StartWallJumpCoyoteTime()
    {

        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = false;
    }

    public void SetIsJumping()
    {
        isJumping = true;
    }

}
