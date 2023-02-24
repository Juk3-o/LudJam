using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashcatfoodablitiy : MonoBehaviour
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

        player = collision.GetComponent<Player>();

        player.playerData.DashAbility = true;

        FindObjectOfType<AudioManager>().Play("CollectFood");

        player.StateMachine.ChangeState(player.CollectUpgrade);

        collected = true;

        gameObject.SetActive(false);
    }

}
