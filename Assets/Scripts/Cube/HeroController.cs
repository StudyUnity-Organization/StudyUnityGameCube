using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HeroController : MonoBehaviour {



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
    private bool _canJump = false;

    private Rigidbody _rigidbody;
    private Vector3 _position;

    public static HeroController CubeScript => _cubeScript;
    private static HeroController _cubeScript;



    private void Awake() {
        if (_cubeScript == null) {
            _cubeScript = this;
        } else {
            Destroy(this);
        }
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxAngularVelocity = Mathf.Infinity;
    }



    private void Update() {
        GameOverBorders();
        LogicScript.Logic.ChangingColor();
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
            LogicScript.Logic.GameOver();
        }
    }

    //public void RotationCube() {
    //    float rotationCube = Input.GetAxis("Horizontal") * speedRotation;
    //    _rigidbody.AddTorque(0, rotationCube, 0);
    //}
    public void RotationCube() {
        float rotationCube = Input.GetAxis("Horizontal") * angleRotation * Time.deltaTime;
        Quaternion turning = Quaternion.AngleAxis(rotationCube, Vector3.up);
        _rigidbody.transform.rotation *= turning;
    }


    //public void DisplacementCube() {
    //    float displacemenCube = Input.GetAxis("Vertical") * speedDisplacement;
    //    _rigidbody.AddRelativeForce(0, 0, displacemenCube, ForceMode.Impulse);
    //}

    public void DisplacementCube() {
        float displacemenCube = speed * Time.deltaTime * Input.GetAxis("Vertical");
        _rigidbody.transform.position = new Vector3(_rigidbody.transform.position.x + transform.forward.x * displacemenCube,
                                                    _rigidbody.transform.position.y,
                                                    _rigidbody.transform.position.z + transform.forward.z * displacemenCube);
    }

    public void JumpCube() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (_canJump) {
                _rigidbody.AddForce(Vector3.up * forceJump, ForceMode.Acceleration);
            }
        }
    }

    public void TryAllowJump(string tag) {
        if (tag.Equals("Platform")) {
            _canJump = !_canJump;
        }
    }

    private void OnCollisionExit(Collision collision) {
        string tag = collision.gameObject.tag;
        TryAllowJump(tag);
    }

    private void OnCollisionEnter(Collision collision) {
        string tag = collision.gameObject.tag;
        TryAllowJump(tag);

        if (tag.Equals("Wall")) {
            LogicScript.Logic.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other) {
        string tag = other.gameObject.tag;
        if (tag.Equals("Target")) {
            LogicScript.Logic.SpawnCubeGeneator();
            LogicScript.Logic.ScorePlus(1);
            Destroy(other.gameObject);
        }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

}
