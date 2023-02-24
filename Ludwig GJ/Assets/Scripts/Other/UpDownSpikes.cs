using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class UpDownSpikes : MonoBehaviour
{


    public float amp;
    public float speed;

    Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    private void Update()
    {

        transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * speed) * amp + initPos.y, 0);

    }
}

