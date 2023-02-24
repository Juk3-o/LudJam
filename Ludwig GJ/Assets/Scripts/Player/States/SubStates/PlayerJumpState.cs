using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string aniBoolName) : base(player, stateMachine, playerData, aniBoolName)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }


    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        Movement?.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;


        player.Height.GetComponentInChildren<Animator>().SetTrigger("SJump");

       if (amountOfJumpsLeft == playerData.amountOfJumps)
        {
           player.ParticleManager?.StartParticles(player.JumpParticles);
        }

        player.Jump();

        amountOfJumpsLeft--;

        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void resetAmountOfJumpsLeft()
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public void decreaseAmountOfJumpsLeft()
    {
        Debug.Log(amountOfJumpsLeft + "jumps");
        amountOfJumpsLeft--;
    }

}
