using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollectable : MonoBehaviour
{
    public float amp;
    public float speed;

    Vector3 initPos;

    Player player;

    public static float fishCollected;

    private void Start()
    {
        initPos = transform.position;

        fishCollected = 0;

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

            player.StateMachine.ChangeState(player.FishCollectState);

            FindObjectOfType<AudioManager>().Play("CollectFish");

            fishCollected++;

            Destroy(gameObject);
        }


    }

}