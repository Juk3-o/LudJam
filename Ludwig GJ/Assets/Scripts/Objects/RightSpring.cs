using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightSpring : MonoBehaviour
{

    [SerializeField] private float bounceAmount;
    [SerializeField] private Vector2 angle;

    [SerializeField] private float cantControlPlayerTime;


    private float startTime;

    Player player;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        PlayerInAirState.canMoveInAir = false;

        startTime = Time.time;


        player = collision.GetComponent<Player>();

        player.Movement?.SetVelocity(bounceAmount, angle);

        animator.SetTrigger("action");

    }

    private void Update()
    {
        
        if (Time.time > startTime + cantControlPlayerTime)
        {
            PlayerInAirState.canMoveInAir = true;
        }

    }

}
