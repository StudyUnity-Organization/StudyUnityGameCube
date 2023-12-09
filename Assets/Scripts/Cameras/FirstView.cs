using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstView : MonoBehaviour
{
    [SerializeField]
    private GameObject camFirstPersonView;
    [SerializeField]
    private float angleFirstViewX = 30f;
    [SerializeField]
    private float angleFirstViewY = 15f;

    private float _xRotation;
    private float _yRotation;


    public void FirstViweCameraRotation(GameObject cube ,float turnX, float turnY) {
        Quaternion cubeRotation = new Quaternion(0, cube.transform.rotation.y, 0, cube.transform.rotation.w);
        _xRotation = Interpolation.Remap3D(0, 1, 0, angleFirstViewY, -turnY);
        _yRotation = Interpolation.Remap3D(0, 1, 0, angleFirstViewX, turnX);
        camFirstPersonView.transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0) * cubeRotation;
        camFirstPersonView.transform.position = cube.transform.position;

    }


}
