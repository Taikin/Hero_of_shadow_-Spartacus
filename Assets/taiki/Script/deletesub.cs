using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deletesub : MonoBehaviour {
    private float spaDeleteflg;
    public float spaDeleteFlg { set { spaDeleteflg = value; } }
    Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (spaDeleteflg != 0)
        {
            animator.SetBool("deletesub", true);
        }
	}
}
