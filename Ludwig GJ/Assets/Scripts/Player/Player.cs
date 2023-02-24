using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Movement Movement { get => movement ?? Core.GetCoreComponent(ref movement); }
    private Movement movement;
    public CollisionSenses CollisionSenses { get => collisionSenses ?? Core.GetCoreComponent(ref collisionSenses); }
    private CollisionSenses collisionSenses;

    public MyParticleManager ParticleManager { get => particleManager ?? Core.GetCoreComponent(ref particleManager); }
    private MyParticleManager particleManager;

    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D RB { get; private set; }

    [SerializeField]
    public PlayerData playerData;

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerLedgeClimbState LedgeClimbState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public SleepingState SleepState { get; private set; }
    public CollectUpgrade CollectUpgrade { get; private set; }
    public FishCollectState FishCollectState { get; private set; }

    [NonSerialized] public bool IsDashing;

    public Transform Height;

    [NonSerialized] public HitStop hitstop;

    [SerializeField] public GameObject DustLandParticles;
    [SerializeField] public GameObject TurnParticles;
    [SerializeField] public GameObject JumpParticles;

    public static int DeathCount;

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();

        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, playerData, "inAir");
        LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData, "ledgeClimbState");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
        SleepState = new SleepingState(this, StateMachine, playerData, "sleep");
        CollectUpgrade = new CollectUpgrade(this, StateMachine, playerData, "upgrade");
        FishCollectState = new FishCollectState(this, StateMachine, playerData, "fish");
    }


    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();
        
        StateMachine.initalise(SleepState);
        IsDashing = false;

        GameObject manager = GameObject.FindWithTag("Manager");

        if (manager)
        {
            hitstop = manager.GetComponent<HitStop>();
        }

        FindObjectOfType<AudioManager>().Play("SewersSound");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        DeathCount = 0;

        playerData.DashAbility = false;

        playerData.WallClimbAbility = false;

    }

    private void Update()
    {
        Core.LogicUpdate();
        StateMachine.CurrentState.LogicUpdate();

        if (playerData.DoubleJumpAbility)
        {
            playerData.amountOfJumps = 2;
        }
        else
        {
            playerData.amountOfJumps = 1;
        }
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void AnimationTrigger() { StateMachine.CurrentState.AnimationTrigger(); }

    private void AnimationFinishTrigger() { StateMachine.CurrentState.AnimationFinishTrigger(); }

    public void RespawnTrigger()
    {

        gameObject.transform.position = RespawnZone.SpawnPoint;

        DeathCount++;

        Trap.isDying = false;
        
        RB.bodyType = RigidbodyType2D.Dynamic;

        Anim.SetTrigger("respawn");

        FindObjectOfType<AudioManager>().Play("Respawn");

    }

    public void RespawnFin()
    {

        InputHandler.CanUseInput = true;

        StateMachine.ChangeState(IdleState);

    }


    private float stepRate = 0.5f;
    private float stepCoolDown;

    public void Footsteps()
    {
        stepCoolDown -= Time.deltaTime;

        if (stepCoolDown < 0)
        {
            FindObjectOfType<AudioManager>().Play("FootSteps");
            stepCoolDown = stepRate;
        }

    }

    public void Land()
    {

        FindObjectOfType<AudioManager>().Play("FootSteps");

    }

    public void Sleep()
    {

        FindObjectOfType<AudioManager>().Play("CatSleep");

    }

    public void StopSleep()
    {

        FindObjectOfType<AudioManager>().Stop("CatSleep");

    }

    public void Wake()
    {
        FindObjectOfType<AudioManager>().Play("Wake");
    }    
    
    public void Music()
    {
        FindObjectOfType<AudioManager>().Play("Music");
    }

    public void Dash()
    {
        FindObjectOfType<AudioManager>().Play("Dash");

    }

    public void Jump()
    {
        FindObjectOfType<AudioManager>().Play("Jump");

    }

    public void Brush()
    {
        FindObjectOfType<AudioManager>().Play("Brush");
    }

}
