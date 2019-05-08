using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSC_Main : MonoBehaviour {

    public AudioClip MainBGM;
    private AudioSource audiosource;

	// Use this for initialization
	void Start () {
        audiosource = gameObject.GetComponent<AudioSource>();
        audiosource.PlayOneShot(MainBGM);
    }
	
	// Update is called once per frame
	void Update () {
	}
}
