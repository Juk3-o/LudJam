using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedDash : MonoBehaviour
{
    [SerializeField] GameObject dashJumpCatFood;
    Dashcatfoodablitiy dashadd;

    Animator animator;
    private void Start()
    {
        dashadd = dashJumpCatFood.GetComponent<Dashcatfoodablitiy>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (dashadd.collected)
        {
            animator.SetTrigger("Get");
            dashadd.collected = false;
        }
    }

    public void Disable()
    {
        Destroy(gameObject);
    }
}
