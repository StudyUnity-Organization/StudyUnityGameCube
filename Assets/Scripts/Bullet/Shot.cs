using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : MonoBehaviour
{
    [SerializeField]
    private GameObject Cube;
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private float Power;
    [SerializeField]
    private Sprite SpriteBlack;
    [SerializeField]
    private Sprite SpriteRed;
    [SerializeField]
    private Sprite SpriteGreen;

    private GameObject _bullet;  //CubeGeneratorClon
    private Image _image;
    private RaycastHit _hit;

    void Start() {
       // Debug.Log("Sprite _____ " + UI.Ui.Aim.GetComponent<Image>().sprite.name);
        _image = UI.Ui.Aim.GetComponent<Image>();
        //_spriteBlack = Resources.Load<Sprite>("aimBlack") as Sprite; ;
        //_spriteRed = Resources.Load<Sprite>("aimRed") as Sprite; ; 
    }
    

    void Update() {       
        if (LogicScript.Logic.StartGame) {
            if (Input.GetMouseButtonDown(0)) {    
                _bullet = Instantiate(Bullet, Cube.transform.position + Cube.transform.forward, Cube.transform.rotation * Quaternion.Euler(0, 90, 0));
                _bullet.GetComponent<Rigidbody>().AddForce((Cube.transform.forward * 3) * Power, ForceMode.Impulse);

            }
        }
        
        TargetRecognition();
    }


    private void TargetRecognition() {
        Ray ray = new Ray(Cube.transform.position, Cube.transform.forward * 100);
        Debug.DrawRay(Cube.transform.position,  Cube.transform.forward*100, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(ray, out _hit)) {
            if(_hit.collider.gameObject.CompareTag("TargetGun"))
                //_image.sprite = SpriteRed;
                _image.sprite = SpriteGreen;
        } else {
            _image.sprite = SpriteBlack;
        }

    }
}
