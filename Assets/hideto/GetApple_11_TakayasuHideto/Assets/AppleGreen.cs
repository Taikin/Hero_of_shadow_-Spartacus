using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleGreen : MonoBehaviour {


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // フレームごとに落下
        transform.Translate(0, -0.1f, 0);

        // 画面外に出たらオブジェクトを破壊
        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);
        }
    }
}
