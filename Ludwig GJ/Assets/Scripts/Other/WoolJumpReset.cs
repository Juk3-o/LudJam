using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoolJumpReset : MonoBehaviour
{
    Animator animator;
    Player player;

    BoxCollider2D boxCollider;

    HitStop hitstop;

    public float amp;
    public float speed;

    Vector3 initPos;


    private void Start()
    {
        animator = GetComponent<Animator>();

        boxCollider = GetComponent<BoxCollider2D>();

        GameObject manager = GameObject.FindWithTag("Manager");
        
        initPos = transform.position;

        if (manager)
        {
            hitstop = manager.GetComponent<HitStop>();
        }

    }

    private void Update()
    {

        transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * speed) * amp + initPos.y, 0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();

            player.JumpState.resetAmountOfJumpsLeft();

            FindObjectOfType<AudioManager>().Play("Wool");

            animator.SetTrigger("hit");

            hitstop.Freeze();

            boxCollider.enabled = false;
        }
    }

    public void animationEvent()
    {
        boxCollider.enabled = true;
    }

}
