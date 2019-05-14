﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load1_Main : MonoBehaviour {
    float LoadTime;
    public Material[] _material;           // 割り当てるマテリアル. 

    public float z1;

    public int flg1;

    // Use this for initialization
    void Start()
    {
        LoadTime = 0.0f;
        flg1 = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(z1, 0.0f, 0.0f);
        LoadTime += Time.deltaTime;
        if (transform.position.x < -2.17f)
        {
            transform.position = new Vector3(3.2f, 0.61f, -9.5f);
        }

        if (LoadTime > 40)
        {
            flg1++;
            LoadTime = 0;
        }

        if (flg1 > 2)
        {
            flg1 = 2;
        }

        this.GetComponent<MeshRenderer>().material = _material[flg1];

    }
}