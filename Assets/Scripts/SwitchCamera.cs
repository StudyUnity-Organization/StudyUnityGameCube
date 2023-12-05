using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class SwitchCamera : MonoBehaviour {
    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private GameObject camFirstPersonView;
    [SerializeField]
    private GameObject camThirdPersonView;
    [SerializeField]
    private Vector2 turn;

    [SerializeField]
    private float radius = -7.6f;
    [SerializeField]
    private float angleFirstViewX = 30f;
    [SerializeField]
    private float angleFirstViewY = 15f;
    [SerializeField]
    private float speedRotationFirstView = 0.1f;
    [SerializeField]
    private float speedRotationThirdView = 0.01f;

    private bool _swich = true;
    private float _xRotation;
    private float _yRotation;
    private float _speedRotation;


    [SerializeField]
    private Vector3 rotationInSpereCoordinate;
    [SerializeField]
    private Vector3 rotationInCartesianCoordinate;

    void Update() {
        if (Input.GetKeyDown(KeyCode.V)) {
            _swich = !_swich;
            SwitchCameraView();
        }
        if (Input.GetKey(KeyCode.Mouse0)) {
            turn.x += Input.GetAxis("Mouse X") * _speedRotation;
            turn.y += Input.GetAxis("Mouse Y") * _speedRotation;
            if (turn.x >= 1) {
                turn.x = 1;
            } else if (turn.x <= -1) {
                turn.x = -1;
            }
            if (turn.y >= 1) {
                turn.y = 1;
            } else if (turn.y <= -1) {
                turn.y = -1;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            if (!_swich) {
                DefoltPositionThirdViewCamera();
            } else {
                turn.x = 0f;
                turn.y = 0f;
            }
        }
        RotationCameraView();

    }

    private void DefoltPositionThirdViewCamera() {
        camThirdPersonView.transform.position = new Vector3(0, 3, -7);
        rotationInCartesianCoordinate = RotationAroundCartesian(0, 3, -7);
        radius = rotationInCartesianCoordinate.x;
        turn.x = Interpolation.Remap3D(0, 360, 0, 1, rotationInCartesianCoordinate.y);
        turn.y = -Interpolation.Remap3D(0, 360, 0, 1, rotationInCartesianCoordinate.z);
    }

    private void SwitchCameraView() {
        camFirstPersonView.SetActive(_swich);
        camThirdPersonView.SetActive(!_swich);
        if (!_swich) {
            DefoltPositionThirdViewCamera();
        } else {
            turn.x = 0f;
            turn.y = 0f;
        }
    }

    private void RotationCameraView() {
        Quaternion cubeRotation = new Quaternion(0, cube.transform.rotation.y, 0, cube.transform.rotation.w);
        if (_swich) {
            _speedRotation = speedRotationFirstView;
            _xRotation = Interpolation.Remap3D(0, 1, 0, angleFirstViewY, -turn.y);
            _yRotation = Interpolation.Remap3D(0, 1, 0, angleFirstViewX, turn.x);
            camFirstPersonView.transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0) * cubeRotation;
            camFirstPersonView.transform.position = cube.transform.position;
        } else {
            _speedRotation = speedRotationThirdView;
            _xRotation = Interpolation.Remap3D(0, 1, 0, 360, turn.x);
            _yRotation = Interpolation.Remap3D(0, 1, 0, 360, -turn.y);

            rotationInSpereCoordinate = RotationAroundSphere(radius, _xRotation, _yRotation);
            if (rotationInSpereCoordinate.y < 0) {
                rotationInSpereCoordinate.y = 0;
            }
            camThirdPersonView.transform.position = rotationInSpereCoordinate + cube.transform.position;

            Vector3 relativePos = cube.transform.position - camThirdPersonView.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            camThirdPersonView.transform.rotation = rotation;
        }
    }

    private Vector3 RotationAroundSphere(float p, float angleTh, float angleF) {
        angleTh = angleTh / Mathf.Rad2Deg;
        angleF = angleF / Mathf.Rad2Deg;
        float x = p * Mathf.Sin(angleTh) * Mathf.Cos(angleF);
        float y = p * Mathf.Sin(angleTh) * Mathf.Sin(angleF);
        float z = p * Mathf.Cos(angleTh);
        return new Vector3(x, y, z);
    }
    private Vector3 RotationAroundCartesian(float x, float y, float z) {
        float p = x * x + y * y + z * z;
        p = MathF.Sqrt(p);
        float angleTh = MathF.Acos(z / p) * Mathf.Rad2Deg;
        float angleF = MathF.Atan2(y, x) * Mathf.Rad2Deg;
        return new Vector3(p, angleTh, angleF);
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.grey;
        Gizmos.DrawSphere(cube.transform.position, radius);
    }

}

