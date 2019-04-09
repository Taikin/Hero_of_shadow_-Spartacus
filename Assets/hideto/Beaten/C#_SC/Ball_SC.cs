using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_SC : MonoBehaviour {

    public float ball_speed = 1000;

    public int store;
    public int Time;
    public float x, y, z;
    bool shotFlg;

    // Use this for initialization
    void Start() {
        x = 0;
        y = 1;
        z = -5;
        shotFlg = false;
        Time = 3;
    }

    // Update is called once per frame
    void Update() {
        this.transform.localPosition = new Vector3(x, y, z);

        if (Input.GetMouseButton(0))
        {
            shotFlg = true;
        }

        if (shotFlg == true)
        {
            if (Time != 0)
            {
                Time--;
            }
            else if (Time == 0)
            {
                Time = 3;
                z += 1;
            }
        }

    }
    void OnCollisionEnter(Collision ball)
    {
        if (ball.gameObject.tag == "Suparutakusu_Beaten")
        {
            Time = 3;
            z = -10;
            shotFlg = false;
        }
    }

}
