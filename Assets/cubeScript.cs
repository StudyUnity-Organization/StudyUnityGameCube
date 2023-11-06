using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cubeScript : MonoBehaviour
{

    public int speed = 5;
    public LogicScript logic;
    public bool can = false;

 
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {

        GameOverBorders();
        // cube_Move(false);                  //TODO+ инвертировать положения для передвижения относительно камеры, чтобы движение было интуитивно понятно 
        if (can) {
            cube_Move(can);
        }


    }
    void GameOverBorders()
    {
        if (transform.position.y < -1) {
            logic.GameOver();
        }

    }

    public void cube_Move(bool can)
    {
        if (can)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z + speed * Time.deltaTime);

            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z - speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z + speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z - speed * Time.deltaTime);
            }
        }



    }



}
