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
    private FirstView FirstView;
    [SerializeField]
    private ThirdVeiw ThirdView;
    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private GameObject camFirstPersonView;
    [SerializeField]
    private GameObject camThirdPersonView;

    [SerializeField]
    private float speedCamerRotationFirst = 0.1f;
    [SerializeField]
    private float speedCamerRotationThird = 0.01f;

    private Vector2 _turn;    
    private bool _swich = true;

    private float _speedCamerRotation;

    void Update() {
        SpeedRotation();
        if (Input.GetKeyDown(KeyCode.V)) {
            _swich = !_swich;          
            SwitchCameraView();
        }
        if (Input.GetKey(KeyCode.Mouse0)) {
            _turn.x += Input.GetAxis("Mouse X") * _speedCamerRotation;
            _turn.y += Input.GetAxis("Mouse Y") * _speedCamerRotation;
            if (_turn.x >= 1) {
                _turn.x = 1;
            } else if (_turn.x <= -1) {
                _turn.x = -1;
            }
            if (_turn.y >= 1) {
                _turn.y = 1;
            } else if (_turn.y <= -1) {
                _turn.y = -1;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            if (!_swich) {
                ThirdView.DefoltPositionThirdViewCamera(cube);
            } else {
                _turn.x = 0f;
                _turn.y = 0f;
            }
        }

        if (_swich) {
            FirstView.FirstViweCameraRotation(cube, _turn.x, _turn.y);
        } else {
            ThirdView.ThirdViweCameraRotation(cube, _turn.x, _turn.y);
        }
    }
    
    private void SwitchCameraView() {
        camFirstPersonView.SetActive(_swich);
        camThirdPersonView.SetActive(!_swich);
        if (!_swich) {       
            ThirdView.DefoltPositionThirdViewCamera(cube);
        } else {
            _turn.x = 0f;
            _turn.y = 0f;
        }
    }

    private void SpeedRotation() {
        if (!_swich) {
            _speedCamerRotation = speedCamerRotationThird;
        } else {
            _speedCamerRotation = speedCamerRotationFirst;           
        }
    }





}

