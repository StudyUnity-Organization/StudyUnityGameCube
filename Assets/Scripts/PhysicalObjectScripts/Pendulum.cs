using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pendulum : MonoBehaviour {
    [SerializeField]
    private float calibrationAngle;
    float _targetVelosity;
    HingeJoint _joint;
    JointMotor _jointMotor;
    Transform _transform;
    bool _isWorked = true;
    float max;
    float min;
    float rotationAnglePendulum;
    float _jointMax;
    float _jointMin;
    // Start is called before the first frame update
    void Start() {
        _jointMotor = gameObject.GetComponent<HingeJoint>().motor;
        _transform = gameObject.transform;
        _joint = gameObject.GetComponent<HingeJoint>();
        max = _joint.limits.max - _joint.limits.min - calibrationAngle;
        min = _joint.limits.max + _joint.limits.min + calibrationAngle;
        _jointMax = _joint.limits.max;
        _jointMin = _joint.limits.min;
        Debug.Log("max " + max);
        Debug.Log("min " + min);
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log(_jointMotor.targetVelocity);
        //Debug.Log(_transform.eulerAngles.z-1);
        //if (_transform.eulerAngles.z + 1 >= _joint.limits.max && !_isWorked || 
        //    _transform.eulerAngles.z + 1 >= 360 + _joint.limits.min && _isWorked) {
        //    Debug.Log("сработала");
        //    _isWorked = !_isWorked;
        //    Debug.Log(_isWorked);
        //    // Debug.Log(_jointMotor.targetVelocity);
        //    _jointMotor.targetVelocity *= -1.0f;
        //    gameObject.GetComponent<HingeJoint>().motor = _jointMotor;
        //  //  _jointMotor.targetVelocity = _jointMotor.targetVelocity  * -1;
        // //   Debug.Log(_jointMotor.targetVelocity);
        //}
        rotationAnglePendulum = (_transform.eulerAngles.z + _jointMax) % 360;
       // Debug.Log(rotationAnglePendulum);
        //140 > значения    знаячение > 0

        if (max < rotationAnglePendulum && _isWorked) {
            _isWorked = !_isWorked;
            _jointMotor.targetVelocity *= -1.0f;
            gameObject.GetComponent<HingeJoint>().motor = _jointMotor;
        }
        if (min > rotationAnglePendulum  && !_isWorked) {
            _isWorked = !_isWorked;
            _jointMotor.targetVelocity *= -1.0f;
            gameObject.GetComponent<HingeJoint>().motor = _jointMotor;
        }

        // if ((max < rotationAnglePendulum 
        //     && rotationAnglePendulum> min)) {
        //     Debug.Log("сработала");
        //     _isWorked = !_isWorked;
        ////     Debug.Log(_isWorked);
        //     // Debug.Log(_jointMotor.targetVelocity);
        //     _jointMotor.targetVelocity *= -1.0f;
        //     gameObject.GetComponent<HingeJoint>().motor = _jointMotor;
        //     //  _jointMotor.targetVelocity = _jointMotor.targetVelocity  * -1;
        //     //   Debug.Log(_jointMotor.targetVelocity);
        // }

    }

    private void OnCollisionEnter(Collision collision) {
        string tag = collision.gameObject.tag;
      
        switch (tag) {
            case "Player": {
                    Debug.Log(transform.position * 1000);
                    collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(-transform.position*100, HeroController.CubeScript.GetPosition());
             //         -(new Vector3(collision .point transform.position.x*100, transform.position.y, transform.position.z*100) ));
                }
                break;
        }

    }
}
