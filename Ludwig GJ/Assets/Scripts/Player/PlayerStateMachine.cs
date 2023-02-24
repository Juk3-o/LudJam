using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; }
    public PlayerState PreviousState { get; private set; }
    public PlayerState PreviousPreviousState { get; private set; }

    public void initalise(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();
        PreviousPreviousState = PreviousState;
        PreviousState = CurrentState;
        CurrentState = newState;
        CurrentState.Enter();

       
    }
}
