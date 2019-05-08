using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharctorMovement : MonoBehaviour {
    [SerializeField,Header("スパルタクスの走る速度")]
    private int suparutakusu_move = 0;
    private Vector3 force;
    public int Force        //スパルタクス(実体)の走る速度を変える
    {
        get { return suparutakusu_move; }
        set { suparutakusu_move = value; }
    }

    Rigidbody rb;

	void Start ()
    {
        
    }
	
	void FixedUpdate()
    {
        rb = GetComponent<Rigidbody>();
        force = new Vector3(suparutakusu_move,0,0);
        rb.AddForce(force);
	}
}