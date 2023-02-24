using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenFloor : MonoBehaviour
{

    Rigidbody2D rb;

    Player player;

    private Vector3 op;
    private Quaternion rp;

    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay = 0.002f;
    public float shake_intensity = .3f;

    private float temp_shake_intensity = 0;

    private int timesHit;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        timesHit = 0;

        op = transform.position;
        rp = transform.rotation;

    }


    void Update()
    {
        if (temp_shake_intensity > 0)
        {
            transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
            transform.rotation = new Quaternion(
                originRotation.x + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.y + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.z + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
                originRotation.w + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f);
            temp_shake_intensity -= shake_decay;
        }

        if(Trap.isDying)
        {
            rb.bodyType = RigidbodyType2D.Static;
            transform.position = op;
            transform.rotation = rp;
            timesHit = 0;
        }
    }

    void Shake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        temp_shake_intensity = shake_intensity;


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(timesHit == 0)
        {
            if (collision.gameObject.CompareTag("Player"))
            {

                timesHit++;

                player = collision.gameObject.GetComponent<Player>();

                FindObjectOfType<AudioManager>().Play("FloorBreak");

                Shake();

                if(player.StateMachine.CurrentState == player.LedgeClimbState)
                {
                    player.StateMachine.ChangeState(player.InAirState);
                }

                rb.bodyType = RigidbodyType2D.Dynamic;

                Shake();
            }
        }


    }
}
