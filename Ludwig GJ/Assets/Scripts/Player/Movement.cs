using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D RB { get; private set; }

    public int FacingDirection { get; private set; }

    public bool CanSetVelocity { get; set; }

    public Vector2 CurrentVelocity { get; private set; }

    private Vector2 workspace;

    [NonSerialized] public bool Justflipped;

    protected override void Awake()
    {
        base.Awake();

        RB = GetComponentInParent<Rigidbody2D>();

        FacingDirection = 1;
        CanSetVelocity = true;
        Justflipped = false;
    }

    public override void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
    }

    #region Set Function

    public void SetVelocityZero()
    {
        workspace = Vector2.zero;
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        SetFinalVelocity();

    }

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        SetFinalVelocity();
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(0, velocity);
        SetFinalVelocity();
    }

    private void SetFinalVelocity()
    {
     

        if (CanSetVelocity)
        {
            RB.velocity = workspace;
            CurrentVelocity = workspace;
        }
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public void Flip()
    {
        FacingDirection *= -1;
        RB.transform.Rotate(0.0f, 180.0f, 0.0f);
        Justflipped = true;
    }

    #endregion
}
