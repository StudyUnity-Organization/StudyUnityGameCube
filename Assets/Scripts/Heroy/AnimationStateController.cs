using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{

    private Animator animator;
    private bool isWalk;
    private bool isRun;
    private bool isJump;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        isWalk = Input.GetKey(KeyCode.W);
        isRun = Input.GetKey(KeyCode.LeftShift);
        isJump = Input.GetKey(KeyCode.Space);
        //Debug.Log("isWalk = " + isWalk);
        //Debug.Log("isRun = " + isRun);
        Debug.Log("isJump = " + isJump);
        if (isWalk) {
            if (isRun) {
                animator.SetBool("isRun", true);
            } else {
                animator.SetBool("isRun", false);
                animator.SetBool("isWalking", true);
            }
        }

        if (isJump) {
            animator.SetBool("isJump", true);
        } else {
            animator.SetBool("isJump", false);
        }

        if (!isWalk) {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRun", false);
        }
    }
}
