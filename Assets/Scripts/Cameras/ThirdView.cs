using CustomMath;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ThirdVeiw : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float radius = -7.6f;

    [SerializeField]
    private Vector3 rotationInSpereCoordinate;
    [SerializeField]
    private Vector3 rotationInCartesianCoordinate;

    private float _xRotation;
    private float _yRotation;
    private GameObject _cube;

    public void DefoltPositionThirdViewCamera(GameObject cube) {
        transform.position = new Vector3(0, 3, -7);
        rotationInCartesianCoordinate = RotationAroundCartesian(0, 3, -7);
        radius = rotationInCartesianCoordinate.x;
        //turn.x = Interpolation.Remap3D(0, 360, 0, 1, rotationInCartesianCoordinate.y);
        //turn.y = -Interpolation.Remap3D(0, 360, 0, 1, rotationInCartesianCoordinate.z);

        ThirdViweCameraRotation(cube, Interpolation.Remap3D(0, 360, 0, 1, rotationInCartesianCoordinate.y), -Interpolation.Remap3D(0, 360, 0, 1, rotationInCartesianCoordinate.z));
    }


    public void ThirdViweCameraRotation(GameObject cube, float turnX, float turnY) {
        _cube = cube;
        _xRotation = Interpolation.Remap3D(0, 1, 0, 360, turnX );
        _yRotation = Interpolation.Remap3D(0, 1, 0, 360, -turnY);
        
              // rotationInSpereCoordinate = RotationAroundSphere(radius, _xRotation, _yRotation);
              rotationInSpereCoordinate = RotationAroundSphere(radius, _xRotation, _yRotation);
        if (rotationInSpereCoordinate.y < 0) {
            rotationInSpereCoordinate.y = 0;
        }

        transform.position = rotationInSpereCoordinate + cube.transform.position;


        //Vector3 rebound = Vector3D.ConversionVector3DInVector3(Vector3D.ReflectionFromThePlaneGlass(Vector3D.ConversionVector3InVector3D(transform.position),
        //                                                                                            Vector3D.ConversionVector3InVector3D(Vector3.one),
        //                                                                                            Vector3D.ConversionVector3InVector3D(Vector3.up) , 1));
        //float distanceBetweenCameraAndReflection = Vector3.Distance(rebound, transform.position);

        //Vector2 rotationAraundCircle = RotationAroundCircle(distanceBetweenCameraAndReflection, _xRotation);
      
        //rotationInSpereCoordinate = new Vector3(rotationAraundCircle.x ,RotationAroundSphere(radius, _xRotation, _yRotation).y, rotationAraundCircle.y);
        //transform.position = rotationInSpereCoordinate + cube.transform.position;

        Vector3 relativePos = cube.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = rotation;


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

    private Vector2 RotationAroundCircle(float p, float angleTh) {
        angleTh = angleTh / Mathf.Rad2Deg;       
        float x = p * Mathf.Cos(angleTh);
        float y = p * Mathf.Sin(angleTh);
        return new Vector2(x, y);
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.grey;
        Gizmos.DrawSphere(_cube.transform.position, radius);
    }
}
