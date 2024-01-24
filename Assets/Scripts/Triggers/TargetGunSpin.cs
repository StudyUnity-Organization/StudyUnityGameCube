using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGunSpin : MonoBehaviour {
    [SerializeField]
    private int health = 10;

    public int _health = 10;

    private void Awake() {
        _health = health;
    }

    public void HitTarget(int damage) {
        _health = _health - damage;
        if (_health <= 0) {
            LogicScript.Logic.SpawnCubeGeneator();
            LogicScript.Logic.ScorePlus(4);
            Destroy(gameObject);
        }

    }
}
