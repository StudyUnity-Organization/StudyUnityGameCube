using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pendulum : MonoBehaviour {

    [SerializeField]
    private float calibrationAngle;

    private HingeJoint _joint;

    private JointMotor _jointMotor;

    private Transform _transform;

    private bool _isWorked = true;

    private float _max;

    private float _min;

    private float rotationAnglePendulum;

    private float _jointMax;

    private float _jointMin;

    // Start is called before the first frame update
    private void Start() {
        _jointMotor = gameObject.GetComponent<HingeJoint>().motor;
        _transform = gameObject.transform;
        _joint = gameObject.GetComponent<HingeJoint>();
        _max = _joint.limits.max - _joint.limits.min - calibrationAngle;
        _min = _joint.limits.max + _joint.limits.min + calibrationAngle;
        _jointMax = _joint.limits.max;
        _jointMin = _joint.limits.min;
        //Debug.Log("max " + _max);
        //Debug.Log("min " + _min);
    }

    // Update is called once per frame
    private void Update() {

        rotationAnglePendulum = (_transform.eulerAngles.z + _jointMax) % 360;
        // Debug.Log(rotationAnglePendulum);
        //140 > значени€    зна€чение > 0

        if (_max < rotationAnglePendulum && _isWorked) {
            _isWorked = !_isWorked;
            _jointMotor.targetVelocity *= -1.0f;
            gameObject.GetComponent<HingeJoint>().motor = _jointMotor;
        }
        if (_min > rotationAnglePendulum && !_isWorked) {
            _isWorked = !_isWorked;
            _jointMotor.targetVelocity *= -1.0f;
            gameObject.GetComponent<HingeJoint>().motor = _jointMotor;
        }


    }

    private void OnCollisionEnter(Collision collision) {
        string tag = collision.gameObject.tag;

        switch (tag) {
            case "Player": {
                    Debug.Log(transform.position * 1000);
                    collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-transform.position * 100, HeroController.CubeScript.GetPosition());
                    //         -(new Vector3(collision .point transform.position.x*100, transform.position.y, transform.position.z*100) ));
                }
                break;
        }

    }
}
