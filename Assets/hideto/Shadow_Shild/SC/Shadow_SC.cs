using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow_SC : MonoBehaviour {

    private float Shadow_Width, Shadow_Length;
    private float Shadow_x, Shadow_y;

	// Use this for initialization
	void Start () {
        Shadow_Width = 1;
        Shadow_Length = 1;
        Shadow_x = 2.5f;
        Shadow_y = 0;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localScale = new Vector2(Shadow_Width, Shadow_Length);
        this.transform.localPosition = new Vector2(Shadow_x, Shadow_y);

    }
}
