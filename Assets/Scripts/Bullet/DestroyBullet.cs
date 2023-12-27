using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {
    private void Update() {
        if (transform.position.x <= -250 || transform.position.x >= 250 || transform.position.z <= -250 || transform.position.z >= 250) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        string tag = collision.gameObject.tag;
        if (tag.Equals("TargetGun")) {
            LogicScript.Logic.SpawnCubeGeneator();
            LogicScript.Logic.ScorePlus(2);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        } else {
            Destroy(gameObject);
        }
        //if (tag.Equals("Platform")) {
        //    Destroy(gameObject);
        //}

    }

}
