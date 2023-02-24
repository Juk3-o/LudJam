using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{

    [SerializeField] private float bounceAmount;

    Player player;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        player = collision.GetComponent<Player>();

        player.Movement?.SetVelocityY(bounceAmount);

        FindObjectOfType<AudioManager>().Play("Spring");

        player.DashState.ResetCanDash();

        animator.SetTrigger("action");

    }


}
