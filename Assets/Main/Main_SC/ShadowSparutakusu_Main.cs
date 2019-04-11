using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSparutakusu_Main : MonoBehaviour {

    private float Shadow_Width, Shadow_Length;
    private float Shadow_x, Shadow_y, Shadow_z;

    // Use this for initialization
    void Start()
    {
        Shadow_Width = 0.1f;
        Shadow_Length = 0.1f;
        Shadow_x = -0.48f;
        Shadow_y = 1.80f;
        Shadow_z = -7.0f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localScale = new Vector2(Shadow_Width, Shadow_Length);
        this.transform.localPosition = new Vector3(Shadow_x, Shadow_y, Shadow_z);

    }
}
