using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject_Main : MonoBehaviour {
    [SerializeField, Header("オブジェクトのスピード")]
    private float ObjSpeeds;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position += new Vector3(ObjSpeeds,0f,0f);
        //if(transform.position.x < -3f)
        //{
        //    Instantiate();
        //}
    }
}
