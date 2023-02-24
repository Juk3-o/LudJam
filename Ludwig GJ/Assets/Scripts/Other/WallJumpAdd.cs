using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class WallJumpAdd : MonoBehaviour
{

    public float amp;
    public float speed;

    public bool collected;

    Vector3 initPos;

    Player player;

    private void Start()
    {
        initPos = transform.position;
        collected = false;
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

            player.playerData.WallClimbAbility = true;

            FindObjectOfType<AudioManager>().Play("CollectFood");

            player.StateMachine.ChangeState(player.CollectUpgrade);

            collected = true;


            gameObject.SetActive(false);
        }

    }


}
