using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPrompt : MonoBehaviour
{

    Player player;
  
    Animator ani;

    public float amp;
    public float speed;

    Vector3 initPos;

    private void Start()
    {
        ani = GetComponent<Animator>();

        initPos = transform.position;

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

            if(player.playerData.DashAbility)
            {
                ani.SetBool("show", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ani.SetBool("show", false);
            ani.SetBool("hide", true);
        }
    }

    public void Disable()
    {
        Destroy(gameObject);
    }

}
