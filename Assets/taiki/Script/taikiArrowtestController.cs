﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taikiArrowtestController : MonoBehaviour {
    Rigidbody2D rb2d;


	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rb2d.AddForce(new Vector2(50, 0));
    }
}