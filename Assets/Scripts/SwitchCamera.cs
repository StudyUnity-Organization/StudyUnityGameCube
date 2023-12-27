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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.V)) {
            SwitchCameraView();
        }
        if (Input.GetKey(KeyCode.Mouse1)) {
            _turn.x = Input.GetAxis("Mouse X");
            _turn.y = Input.GetAxis("Mouse Y");
            RotationCamers();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1)) {
            DefaltPosition();
        }

        // RotationCamers();
    }

    private void SwitchCameraView() {
        _swich = !_swich;
        camFirstPersonView.SetActive(_swich);
        camThirdPersonView.SetActive(!_swich);
        UI.Ui.Aim.SetActive(_swich);
        DefaltPosition();
    }
    private void DefaltPosition() {
        if (!_swich) {
            ThirdView.DefaltPositionThirdViewCamera(cube);

        } else {
            FirstView.FirstViweCameraRotationDefalt(cube);
        }
    }

    private void RotationCamers() {
        if (_swich) {
            FirstView.FirstViweCameraRotation(cube, _turn.x, _turn.y);
        } else {
            ThirdView.ThirdViweCameraRotation(cube, _turn.x, _turn.y);
        }
    }








}

