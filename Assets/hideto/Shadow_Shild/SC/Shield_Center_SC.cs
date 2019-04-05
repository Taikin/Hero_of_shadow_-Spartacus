using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Center_SC : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D Shield_Center)
    {
        Debug.Log("真ん中に当たった");
    }

}
