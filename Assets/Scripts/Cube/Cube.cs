using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cube : MonoBehaviour {

    public LogicScript Logic;

    [SerializeField]
    private float speed = 5; //10       //10
    [SerializeField]
    private float angleRotation = 15f; //100        /100
   
    [SerializeField]
    private float speedDisplacement = 5; //10       /10
    [SerializeField]
    private float speedRotation = 15f; //20         /200
    [SerializeField]
    private float forceJump = 300; //300            /250  + mass 10 drag 1

    public bool Can = false;


    private Rigidbody _rigidbody;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxAngularVelocity = Mathf.Infinity;
    }


    private void Start() {
        Logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    private void Update() {
        GameOverBorders();    
        Logic.ChangingColor();
        if (Can) {
           JumpCube();
        }
    }

    private void FixedUpdate() {        
        if (Can) {
            DisplacementCube();
            RotationCube();
        }
    }



    private void GameOverBorders() {
        if (transform.position.y < -1) {
            Logic.GameOver();
        }
    }

 
    public void JumpCube() {
        if (Input.GetKeyDown(KeyCode.Space)) {
      //      float jumpCube = Input.GetAxis("Horizontal") * forceJump;
            _rigidbody.AddForce(Vector3.up * forceJump, ForceMode.Acceleration);
        }
    }


    public void RotationCube() {
        float rotationCube = Input.GetAxis("Horizontal") * speedRotation;
        _rigidbody.AddTorque(0, rotationCube, 0);  
    }
    //public void RotationCube() {
    //    float rotationCube = Input.GetAxis("Horizontal") * angleRotation * Time.deltaTime;
    //    Quaternion turning = Quaternion.AngleAxis(rotationCube, Vector3.up);
    //    transform.rotation *= turning;
    //}



    //public void DisplacementCube() {
    //    float displacemenCube = Input.GetAxis("Vertical") * speedDisplacement;
    //    _rigidbody.AddRelativeForce(0, 0, displacemenCube, ForceMode.Impulse);
    //}

    public void DisplacementCube() {
        _rigidbody.position = new Vector3(transform.position.x + transform.forward.x * speed * Time.deltaTime * Input.GetAxis("Vertical"),
                                             transform.position.y,
                                             transform.position.z + transform.forward.z * speed * Time.deltaTime * Input.GetAxis("Vertical"));
    }


}
