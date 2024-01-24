using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationStateController : MonoBehaviour {

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

    [SerializeField]
    private Rigidbody[] allRigidBody;
    [SerializeField]
    private GameObject hips;

    public static AnimationStateController AnimatedHeroy => _animatedHeroy;
    private static AnimationStateController _animatedHeroy;


    private void Awake() {
        if (_animatedHeroy == null) {
            _animatedHeroy = this;
        } else {
            Destroy(this);
        }

        for (int i = 0; i < allRigidBody.Length; i++) {
            allRigidBody[i].isKinematic = true;
        }
    }


    private void Start() {
        animator = GetComponent<Animator>();
    }


    private void Update() {
        isWalk = Input.GetKey(KeyCode.W);
        isRun = Input.GetKey(KeyCode.LeftShift);
        isJump = Input.GetKey(KeyCode.Space);
        isAiming = Input.GetKey(KeyCode.Mouse1);
        if (Input.GetKey(KeyCode.O)) MakePhysical();

        if (isWalk) {
            velocity += Time.deltaTime * acceleration;
        }

        if (isWalk) {
            if (isRun) {
                if (velocity > 1f) {
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


        animator.SetBool("isJump", isJump);
        animator.SetBool("isDeath", isDeath);

        if (isAiming) {
            animator.SetLayerWeight(animator.GetLayerIndex("Aiming"), 1f);
        } else {
            animator.SetLayerWeight(animator.GetLayerIndex("Aiming"), 0f);
        }

    }

    public void MakePhysical() {
        hips.SetActive(true);
        for (int i = 0; i < allRigidBody.Length; i++) {
            allRigidBody[i].isKinematic = false;
        }

        GetComponent<Animator>().enabled = false;

        //isDeath = true;
        //isWalk = false;
        //isRun = false;
        //isJump = false;

    }
}
