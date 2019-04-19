using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Friend : MonoBehaviour {

    public float MF; //仲間の速度

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(MF, 0.0f, 0.0f);//x方向に動く
        if (transform.position.x < -3)
        {
            Destroy(gameObject);//xが-3を超えたら消す
        }
	}
}
