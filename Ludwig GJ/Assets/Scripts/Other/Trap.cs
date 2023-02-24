using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    Player player;

    public static bool isDying;

    public void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.GetComponent<Player>();
  
            player.Anim.SetTrigger("death");

            FindObjectOfType<AudioManager>().Play("Death");

            player.InputHandler.CanUseInput = false;

            isDying = true;

            player.InputHandler.NormInputX = 0;

            player.Movement.SetVelocityZero();

            player.RB.bodyType = RigidbodyType2D.Static;


            

        }
        


    }



    private void Update()
    {
       if(isDying)
        {
            if (player != null)
            {
            player.StateMachine.ChangeState(player.IdleState);
            }
        }
    }

}
