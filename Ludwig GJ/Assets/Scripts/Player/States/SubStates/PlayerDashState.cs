using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerDashState : PlayerAbilityState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aniBoolName) : base(player, stateMachine, playerData, aniBoolName)
    {
    }

    public bool CanDash { get; private set; }
    private bool isHolding;

    private float lastDashTime;

    private Vector2 dashDirection;
    private Vector2 lastAIPos; //afterimage


    private bool isTouchingWall;

    private int walldashDirection;

    public override void Enter()
    {
        base.Enter();
        CanDash = false;
        player.InputHandler.UseDashInput();

        player.Height.GetComponentInChildren<Animator>().SetTrigger("SDash");

        isHolding = true;

        dashDirection = Vector2.right * walldashDirection;

        startTime = Time.time;

        player.hitstop.Freeze();

        player.Dash();


    }

    public override void DoChecks()
    {
        base.DoChecks();
        if (CollisionSenses)
        {
            isTouchingWall = CollisionSenses.WallFront;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();



        xInput = player.InputHandler.NormInputX;

        if (!isExitingState)
        {
            if (isHolding)
            {
                isHolding = false;
                startTime = Time.time;
                Movement?.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                player.RB.drag = playerData.drag;
                Movement?.SetVelocity(playerData.dashVelocity, dashDirection);
                PlaceAfterImage();
            }
            else
            {
                Movement?.SetVelocity(playerData.dashVelocity, dashDirection);
                CheckIfShouldPlaceAfterImage();

                if (Time.time >= startTime + playerData.dashTime)
                {
                    player.RB.drag = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }

        if (isTouchingWall && xInput == Movement?.FacingDirection)
        {
            player.RB.drag = 0f;
            isAbilityDone = true;
            lastDashTime = Time.time;
        }


    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if (Vector2.Distance(player.transform.position, lastAIPos) >= playerData.distBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }


    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAIPos = player.transform.position;
    }



    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.dashCooldown;
    }

    public void ResetCanDash() => CanDash = true;

    public void DetermineDashDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            walldashDirection = -Movement.FacingDirection;

        }
        else
        {
            walldashDirection = Movement.FacingDirection;
        }
    }

}
