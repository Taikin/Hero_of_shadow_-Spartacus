using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleForm : MonoBehaviour {

    public GameObject[] Apple;
    public int number;

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnObject", 0.5f, 0.5f);
	}

		// Update is called once per frame
	void Update () {
	}

    void SpawnObject()
    {
        float x = Random.Range(-11, 11f);
        number = Random.Range(0, Apple.Length);
        Instantiate(Apple[number], new Vector3(x, 4, 0), transform.rotation);
        
    }

}
