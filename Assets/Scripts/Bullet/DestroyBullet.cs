using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {
    [SerializeField]
    private int damage = 2;

    [SerializeField]
    private Transform cubeTransform;
    
    private int _health = 2;

    private void Update() {
        if (transform.position.x <= -250 || transform.position.x >= 250 || transform.position.z <= -250 || transform.position.z >= 250) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        string tag = collision.gameObject.tag;
        switch (tag) {
            case "TargetGun": {
                    LogicScript.Logic.SpawnCubeGeneator();
                    LogicScript.Logic.ScorePlus(2);
                    Destroy(collision.gameObject);
                }
                break;
            case "TargetGunSpin": {
                    //  gameObject.GetComponent<Rigidbody>().AddForce(transform.position / Vector3.Distance(HeroController.CubeScript.GetPosition(), collision.gameObject.transform.position));
                    collision.gameObject.GetComponent<Rigidbody>().AddForce(
                        -(transform.position * Vector3.Distance(HeroController.CubeScript.GetPosition(), collision.gameObject.transform.position)));
                    // TargetGunSpin.getTargetGunSpin.HitTarget(damage);  //ВОПРОС ПО СОЗДАНИЮ ОБЪЕКТА - ПОЧЕМУ ПРИ УДАЛЕНИИ ИЗ СКРИПТА НА ОБЪЕКТЕ - ОШИБКА
                    TargetGunSpin targetGunSpin = collision.gameObject.GetComponent<TargetGunSpin>();
                    targetGunSpin._health -= damage; ;
                    if (targetGunSpin._health <= 0) {
                        LogicScript.Logic.SpawnCubeGeneator();
                        LogicScript.Logic.ScorePlus(4);
                        Destroy(collision.gameObject);
                    }
                }
                break;

        }
        Destroy(gameObject);
      

    }

}
