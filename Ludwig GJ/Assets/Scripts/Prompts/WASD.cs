using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD : MonoBehaviour
{
   
    [SerializeField] GameObject SpaceGo;
    SpaceOrA space;
    Animator ani;
   
    public float amp;
    public float speed;

    Vector3 initPos;

    private void Start()
    {
        space = SpaceGo.GetComponent<SpaceOrA>();
        ani = GetComponent<Animator>();

        initPos = transform.position;

    }

    private void Update()
    {
        transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * speed) * amp + initPos.y, 0);

        if(space.nextPrompt)
        {
            ani.SetBool("show", true);
            space.nextPrompt = false;
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
