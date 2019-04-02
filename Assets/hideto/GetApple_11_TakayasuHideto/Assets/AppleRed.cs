using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleRed : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        // フレームごとに落下
        transform.Translate(0, -0.1f, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Floor")
            {
                Destroy(gameObject);
            }
    }
}
