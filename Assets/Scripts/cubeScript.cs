using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeScript : MonoBehaviour {

    public LogicScript Logic;

    [SerializeField]
    private int speed = 5;
    [SerializeField]
    private float angleRotation = 15f;

    public bool Can = false;


    void Start() {
        Logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update() {

        GameOverBorders();
        if (Can) {
            CubeMove();
        }
        Logic.ChangingColor();

    }
    void GameOverBorders() {
        if (transform.position.y < -1) {
            Logic.GameOver();
        }
    }

    public void CubeMove() {
        if (Can) {
            if (Input.GetKey(KeyCode.W)) {
                DisplacementCube();
            }
            if (Input.GetKey(KeyCode.S)) {
                DisplacementCube();
            }
            if (Input.GetKey(KeyCode.A)) {
                RotationCube();
            }
            if (Input.GetKey(KeyCode.D)) {
                RotationCube();
            }
        }     

    }


    public void RotationCube() {
        float rotationCube = Input.GetAxis("Horizontal") * angleRotation * Time.deltaTime;
        Quaternion turning = Quaternion.AngleAxis(rotationCube, Vector3.up);
        transform.rotation *= turning;
    }

    public void DisplacementCube() {
        transform.position = new Vector3(transform.position.x + transform.forward.x * speed * Time.deltaTime * Input.GetAxis("Vertical"),
                                            transform.position.y,
                                            transform.position.z + transform.forward.z * speed * Time.deltaTime * Input.GetAxis("Vertical"));
    }


}
