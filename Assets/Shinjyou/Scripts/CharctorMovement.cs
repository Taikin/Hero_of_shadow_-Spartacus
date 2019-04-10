using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharctorMovement : MonoBehaviour {
    private float movez = 1f;
    private float speed = 3f;
    Rigidbody rb;

	void Start ()
    {
        
    }
	
	void Update ()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 force = new Vector3(200,0,0);
        rb.AddForce(force);
	}
}