using UnityEngine;

public class cubeScript : MonoBehaviour {
    public LogicScript logic;


    [SerializeField]
    private int speed = 5;
    [SerializeField]
    private float angleRotationX = 15f;

    public bool can = false;


    // public float angleRotationZ = -15f;

    // private Rigidbody _rigidbody;

    void Start() {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update() {

        GameOverBorders();
        if (can) {
            cube_Move(can);
        }

    }
    void GameOverBorders() {
        if (transform.position.y < -1) {
            logic.GameOver();
        }

    }

    public void cube_Move(bool can) {
        if (can) {
            if (Input.GetKey(KeyCode.W)) {
                //  transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z + speed * Time.deltaTime)
                transform.position = new Vector3(transform.position.x + transform.forward.x * speed * Time.deltaTime,
                                                 transform.position.y,
                                                 transform.position.z + transform.forward.z * speed * Time.deltaTime);

                //    _rigidbody.mass = 0f;

            }
            if (Input.GetKey(KeyCode.S)) {
                //   transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z - speed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x - transform.forward.x * speed * Time.deltaTime,
                                                 transform.position.y,
                                                 transform.position.z - transform.forward.z * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A)) {
                //transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z + speed * Time.deltaTime);
                rotationCubeLeftX(gameObject);
                //     transform.position = RotationX(gameObject);



            }
            if (Input.GetKey(KeyCode.D)) {
                //transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z - speed * Time.deltaTime);
                rotationCubeRightX(gameObject);

            }
        }



    }
    //public void startPosition() {
    //    _rigidbody = GetComponent<Rigidbody>();     
    //    _rigidbody.mass = 0f;
    //    transform.position = new Vector3(0, 0, 0);
    //    transform.localScale = new Vector3(1, 1, 1);
    //    _rigidbody.mass = 1f;

    //}


    public void rotationCubeLeftX(GameObject cube) {
        float tiltAroundLeftX = angleRotationX * Input.GetAxis("Horizontal") * Time.deltaTime;
        Quaternion targetAngleLeftX = Quaternion.AngleAxis(tiltAroundLeftX, Vector3.up);
        transform.rotation *= targetAngleLeftX;
    }

    public void rotationCubeRightX(GameObject cube) {
        float angleRotationXRight = angleRotationX * -1f;
        float tiltAroundRightX = -1f * (angleRotationXRight * Input.GetAxis("Horizontal") * Time.deltaTime);
        Quaternion targetAngleRightX = Quaternion.AngleAxis(tiltAroundRightX, Vector3.up);
        transform.rotation *= targetAngleRightX;
    }
}
