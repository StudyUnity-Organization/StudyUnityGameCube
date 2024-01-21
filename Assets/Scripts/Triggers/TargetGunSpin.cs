﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGunSpin : MonoBehaviour
{
    [SerializeField]
    private int health = 10;   

    public int _health = 10; //переменные названы неправильно из-за вопроса!

    private void Awake() {
        _health = health;       
    }

    // Start is called before the first frame update
    void Start()
    {
    
       // _health = health;
    }

    // Update is called once per frame
    void Update()
    {
        
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