using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour {
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float power;


    private GameObject _bullet;  //CubeGeneratorClon  
    private RaycastHit _hit;




    private void Update() {
        if (LogicScript.Logic.StartGame) {
            if (Input.GetMouseButtonDown(0)) {
                _bullet = Instantiate(bullet, transform.position + transform.forward, transform.rotation * Quaternion.Euler(0, 90, 0));
                _bullet.GetComponent<Rigidbody>().AddForce((transform.forward * 3) * power, ForceMode.Impulse);
            }
        }
        TargetRecognition();
    }


    private void TargetRecognition() {
        Ray ray = new Ray(transform.position, transform.forward * 100);
        Debug.DrawRay(transform.position, transform.forward * 100, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(ray, out _hit)) {
            if (_hit.collider.gameObject.CompareTag("TargetGun"))
                UI.UiSpace.HandleTarget(true);
        } else {
            UI.UiSpace.HandleTarget(false);
        }

    }
}
