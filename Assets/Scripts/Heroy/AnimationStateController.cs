using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationStateController : MonoBehaviour
{

    private Animator animator;
    private bool isWalk;
    private bool isRun;
    private bool isJump;
    private bool isDeath;
    private bool isAiming;

    private float velocity = 0.0f;
    [SerializeField]
    private float acceleration = 0.1f;
    [SerializeField]
    private float deceleration = 0.5f;
    private float _transitions = 0.5f;

    public static AnimationStateController AnimatedHeroy => _animatedHeroy;
    private static AnimationStateController _animatedHeroy;


    private void Awake() {
        if (_animatedHeroy == null) {
            _animatedHeroy = this;
        } else {
            Destroy(this);
        }
    }


    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        isWalk = Input.GetKey(KeyCode.W);
        isRun = Input.GetKey(KeyCode.LeftShift);
        isJump = Input.GetKey(KeyCode.Space);
        //isDeath = Input.GetKey(KeyCode.D);
        isAiming = Input.GetKey(KeyCode.Mouse1);

        //Debug.Log("isWalk = " + isWalk);
        //Debug.Log("isRun = " + isRun);
        //Debug.Log("isJump = " + isJump);
        //if (isWalk) {
        //    if (isRun) {
        //        animator.SetBool("isRun", true);
        //    } else {
        //        animator.SetBool("isRun", false);
        //        animator.SetBool("isWalking", true);
        //    }
        //}

        //if (isJump) {
        //    animator.SetBool("isJump", true);
        //} else {
        //    animator.SetBool("isJump", false);
        //}

        //if (!isWalk) {
        //    animator.SetBool("isWalking", false);
        //    animator.SetBool("isRun", false);
        //}


        if (isWalk) {
           velocity += Time.deltaTime * acceleration;
        }

        if (isWalk) {
            if (isRun) {
                if (velocity > 1f){
                    velocity = 1f;
                } else {
                    velocity += Time.deltaTime * acceleration;
                }
            }
            if (!isRun && velocity >= _transitions) {
                velocity -= Time.deltaTime * deceleration;
            } else {
                velocity += Time.deltaTime * acceleration;
            }
        }

        if (!isRun && velocity > _transitions) {
            velocity -= Time.deltaTime * deceleration;
        }

        if (!isWalk && velocity > 0) {            
            velocity -= Time.deltaTime * deceleration;
        }

        animator.SetFloat("Velocity", velocity);



        if (isJump) {
            animator.SetBool("isJump", true);
        } else {
            animator.SetBool("isJump", false);
        }



        if (isDeath) {
            animator.SetBool("isDeath", true);
        } else {
            animator.SetBool("isDeath", false);
        }

        if (isAiming) {
            animator.SetLayerWeight(animator.GetLayerIndex("Aiming"), 1f);
        } else {
            animator.SetLayerWeight(animator.GetLayerIndex("Aiming"), 0f);
        }

    }
}
