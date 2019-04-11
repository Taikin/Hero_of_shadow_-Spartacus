using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTest_SC_Main : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.up * Time.deltaTime * 5;
    }
}
