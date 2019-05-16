using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowsplatkusuDelete : MonoBehaviour {
    float Deletealfa=1;   //影のスパルタクスを透明化させるためのa値


    float Deleteflg;       //矢が刺さった時のフラグ
    float Deletespeed=0.03f; //a値が減算されていく速さ
    Animator shadowanimator;
    SpriteRenderer shadowspriteRenderer;
    Rigidbody2D rb2d;
    float red, green, blue;
	// Use this for initialization
	void Start () {
        shadowanimator = GetComponent<Animator>();
        shadowspriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
      
	}
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            shadowanimator.SetBool("Delete", true);
           // Debug.Log("当たった");
            Deleteflg = 1;
        }
    }
    // Update is called once per frame
    void Update () {
        shadowspriteRenderer.color = new Color(red, green, blue, Deletealfa);

        // Debug.Log(Deleteflg);
        if (Deleteflg != 0)
        {

            Deletealfa -= Deletespeed;
            //Debug.Log(Deletealfa);
        }
    }
}
