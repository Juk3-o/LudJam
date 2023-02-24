using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float wallJumpCoyoteTime = 0.4f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float inAirVelocity = 10f;
    public float rateOfDeceleration = 1;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Ledge Climbe State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Dash State")]
    public float dashCooldown = 0.5f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float drag = 10f;
    public float dashEndYMultiflier = 0.2f;
    public float distBetweenAfterImages = 0.5f;

    [Header("Abilities")]
    public bool DashAbility;
    public bool WallClimbAbility;
    public bool DoubleJumpAbility;
}
