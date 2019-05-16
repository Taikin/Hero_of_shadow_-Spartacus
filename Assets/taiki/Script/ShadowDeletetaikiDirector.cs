
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowDeletetaikiDirector : MonoBehaviour {
    SpriteRenderer sr1; 
    SpriteRenderer sr2;
  
    float red, green, blue; //colorのR,G,B値
    float Deletealfa=1; //フェードアウトさせるためのa値
    float Deletespeed = 0.03f; //a値が減算されていく速さ

    private float Deleteflg;
    public float DeleteFlg { set { Deleteflg = value; } }
    void Start () {
        sr1 = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>(); //子オブジェクト参照
        sr2 = transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();//子オブジェクト参照

    }

    // Update is called once per frame
    void Update () {
  
        sr1.color = new Color(red, green, blue, Deletealfa);
        sr2.color = new Color(red, green, blue, Deletealfa);
        if (Deleteflg != 0)
        {

            Deletealfa -= Deletespeed;
        }
	}
}
