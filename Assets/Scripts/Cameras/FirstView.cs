using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstView : MonoBehaviour
{
    [SerializeField]
    private GameObject camFirstPersonView;
    [SerializeField]
    private float speedCamerRotation = 0.1f;

    [SerializeField]
    private float angleFirstViewX = 30f;
    [SerializeField]
    private float angleFirstViewY = 15f;

    private float _xRotation;
    private float _yRotation;
    private float _turnX;
    private float _turnY;


    public void FirstViweCameraRotation(GameObject cube ,float turnX, float turnY) {
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


        Quaternion cubeRotation = new Quaternion(0, cube.transform.rotation.y, 0, cube.transform.rotation.w);
        _xRotation = Interpolation.Remap3D(0, 1, 0, angleFirstViewY, -_turnY);
        _yRotation = Interpolation.Remap3D(0, 1, 0, angleFirstViewX, _turnX);
        camFirstPersonView.transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0) * cubeRotation;
        camFirstPersonView.transform.position = cube.transform.position;
    }

    public void FirstViweCameraRotationDefalt(GameObject cube) {
        _turnX = 0;
        _turnY = 0;
   
        Quaternion cubeRotation = new Quaternion(0, cube.transform.rotation.y, 0, cube.transform.rotation.w);
        _xRotation = Interpolation.Remap3D(0, 1, 0, angleFirstViewY, -_turnY);
        _yRotation = Interpolation.Remap3D(0, 1, 0, angleFirstViewX, _turnX);
        camFirstPersonView.transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0) * cubeRotation;
        camFirstPersonView.transform.position = cube.transform.position;

     
    }

}
