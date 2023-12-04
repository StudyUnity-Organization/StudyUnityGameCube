using UnityEngine;

public class CubeScript : MonoBehaviour {
    public LogicScript Logic;


    [SerializeField]
    private int speed = 5;
    [SerializeField]
    private float angleRotationX = 15f;

    public bool Can = false;


    void Start() {
        Logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update() {

        GameOverBorders();
        if (Can) {
            ÑubeMove(Can);
        }
        Logic.ChangingColor();

    }
    void GameOverBorders() {
        if (transform.position.y < -1) {
            Logic.GameOver();
        }
    }

    public void ÑubeMove(bool can) {
        if (can) {
            if (Input.GetKey(KeyCode.W)) {
                transform.position = new Vector3(transform.position.x + transform.forward.x * speed * Time.deltaTime,
                                                 transform.position.y,
                                                 transform.position.z + transform.forward.z * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S)) {
                transform.position = new Vector3(transform.position.x - transform.forward.x * speed * Time.deltaTime,
                                                 transform.position.y,
                                                 transform.position.z - transform.forward.z * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A)) {
                RotationCubeLeftX(gameObject);
            }
            if (Input.GetKey(KeyCode.D)) {
                RotationCubeRightX(gameObject);
            }
        }

    }


    public void RotationCubeLeftX(GameObject cube) {
        float tiltAroundLeftX = angleRotationX * Input.GetAxis("Horizontal") * Time.deltaTime;
        Quaternion targetAngleLeftX = Quaternion.AngleAxis(tiltAroundLeftX, Vector3.up);
        cube.transform.rotation *= targetAngleLeftX;
    }

    public void RotationCubeRightX(GameObject cube) {
        float angleRotationXRight = angleRotationX * -1f;
        float tiltAroundRightX = -1f * (angleRotationXRight * Input.GetAxis("Horizontal") * Time.deltaTime);
        Quaternion targetAngleRightX = Quaternion.AngleAxis(tiltAroundRightX, Vector3.up);
        cube.transform.rotation *= targetAngleRightX;
    }

}
