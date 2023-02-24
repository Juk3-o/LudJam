using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpaceOrA : MonoBehaviour
{
    [NonSerialized] public bool nextPrompt;

    [SerializeField] GameObject playerGo;
    private Player player;
    Animator ani;

    private bool canGetUp;

    public float amp;
    public float speed;

    Vector3 initPos;

    private void Start()
    {
        player = playerGo.GetComponent<Player>();
        ani = GetComponent<Animator>();

        initPos = transform.position;

        canGetUp = false;

        nextPrompt = false;
    }

    private void Update()
    {

        transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * speed) * amp + initPos.y, 0);

        if (player.InputHandler.GetUp && canGetUp)
        {
            ani.SetBool("hide", true);
        }
    }

    public void Disable()
    {
        Destroy(gameObject);

        nextPrompt = true;
    }

    public void CanGetUp()
    {
        canGetUp = true;
    }
}
