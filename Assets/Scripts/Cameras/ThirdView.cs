using CustomMath;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ThirdVeiw : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private float speedCamerRotation = 0.01f;

    [SerializeField]
    private float radius = -7.6f;

    [SerializeField]
    private Vector3 rotationInSpereCoordinate;
    [SerializeField]
    private Vector3 rotationInCartesianCoordinate;

    private float _xRotation;
    private float _yRotation;
    private Transform _cubeTransform;
    private Transform _transform;
    private float _turnX;
    private float _turnY;
    private Vector3 _defaltPosition;
    private float _defaltXRotation = 0;
    private float _defaltYRotation = 0;

    private void Awake() {
        _transform = transform;
        _cubeTransform = cube.transform;
    }

    public void DefaltPositionThirdViewCamera(GameObject cube) {
        rotationInCartesianCoordinate = RotationAroundCartesian(0, -7, 3);
        radius = rotationInCartesianCoordinate.x;
        _defaltXRotation = rotationInCartesianCoordinate.y;
        _defaltYRotation = rotationInCartesianCoordinate.z;
        ThirdViweCameraRotationDefalt(cube, rotationInCartesianCoordinate);
    }


    public void ThirdViweCameraRotation(GameObject cube, float turnX, float turnY) {
        _turnX += turnX * speedCamerRotation;
        _turnY += turnY * speedCamerRotation;
        if (_turnX >= 1) {
            _turnX = 1;
        } else if (_turnX <= -1) {
            _turnX = -1;
        }
        if (_turnY >= 1) {
            _turnY = 1;
        } else if (_turnY <= -1) {
            _turnY = -1;
        }
        _xRotation = Interpolation.Remap3D(0, 1, 0, 360, _turnX);
        _yRotation = Interpolation.Remap3D(0, 1, 0, 360, -_turnY);
        // rotationInSpereCoordinate = RotationAroundSphere(radius, _xRotation, _yRotation);
        rotationInSpereCoordinate = RotationAroundSphere(radius, _yRotation + _defaltXRotation, _xRotation + _defaltYRotation);
        if (rotationInSpereCoordinate.y < 0) {
            rotationInSpereCoordinate.y = 0;
        }


        transform.position = rotationInSpereCoordinate + cube.transform.position;
        Vector3 relativePos = _cubeTransform.position - _transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        _transform.rotation = rotation;
    }

    public void ThirdViweCameraRotationDefalt(GameObject cube, Vector3 rotationInCartesianCoordinate) {
        //функция для установки камеры в дефолтное положение относительно куба 
        //беру угол поворота куба по оси у и вычитаю его из дефолтного угла поворота       
        float x = rotationInCartesianCoordinate.x;
        float y = rotationInCartesianCoordinate.y;
        float z = rotationInCartesianCoordinate.z;
        Quaternion rotCube = cube.transform.rotation;
        Vector3 rotCubeAngle = rotCube.ToEulerAngles() / Mathf.Deg2Rad;
        z = z - rotCubeAngle.y;
        //   Debug.Log(z);

        _defaltPosition = RotationAroundSphere(x, y, z);
        //   Vector3 angleBetweenCubeAndCamer = RotationAroundSphere(0, cube.transform.rotation.y, 0);
        transform.position = _defaltPosition + cube.transform.position;

        Vector3 relativePos = cube.transform.position - _transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        _transform.rotation = rotation;
    }

    private Vector3 RotationAroundSphere(float p, float angleTh, float angleF) {
        angleTh = angleTh / Mathf.Rad2Deg;
        angleF = angleF / Mathf.Rad2Deg;
        float sinTh = Mathf.Sin(angleTh);
        float x = p * sinTh * Mathf.Cos(angleF);
        float z = p * sinTh * Mathf.Sin(angleF);
        float y = p * Mathf.Cos(angleTh);
        if (-0.001f < x && x < 0.001f) { x = 0; }
        if (-0.001f < y && y < 0.001f) { y = 0; }
        if (-0.001f < z && z < 0.001f) { z = 0; }

        return new Vector3(x, y, z);
    }
    private Vector3 RotationAroundCartesian(float x, float y, float z) {
        float p = x * x + y * y + z * z;
        p = MathF.Sqrt(p);
        float angleTh = MathF.Acos(z / p) * Mathf.Rad2Deg;
        float angleF = MathF.Atan2(y, x) * Mathf.Rad2Deg;


        return new Vector3(p, angleTh, angleF);

    }

    private Vector2 RotationAroundCircle(float p, float angleTh) {
        angleTh = angleTh / Mathf.Rad2Deg;
        float x = p * Mathf.Cos(angleTh);
        float y = p * Mathf.Sin(angleTh);
        return new Vector2(x, y);
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.grey;
        Gizmos.DrawSphere(_cubeTransform.position, radius);
    }
}
