using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedClimb : MonoBehaviour
{
    [SerializeField] GameObject wallJumpCatFood;
    WallJumpAdd jumpAdd;

    Animator animator;
    private void Start()
    {
        jumpAdd = wallJumpCatFood.GetComponent<WallJumpAdd>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(jumpAdd.collected)
        {
            animator.SetTrigger("Get");
            jumpAdd.collected = false;
        }
    }

    public void Disable()
    {
        Destroy(gameObject);
    }
}
