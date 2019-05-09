using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hose_Main : MonoBehaviour {
    public Animator animator;
    public AudioClip HorseRunnintSE;
    AudioSource audiosorce;
    public float Time;
	// Use this for initialization
	void Start () {
        audiosorce = GetComponent<AudioSource>();
        audiosorce.PlayOneShot(HorseRunnintSE, 0.9F);
        animator = GetComponent<Animator>();
        Time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //Time++;
        //if (Time > 60)
        //{
        //    animator.SetBool("is_Run", false);
        //    animator.SetBool("is_Falldown", true);
        //}
	}
}
