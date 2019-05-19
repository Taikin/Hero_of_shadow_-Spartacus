using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSparutaDilecter_Main : MonoBehaviour {

    SpriteRenderer sr1;
    SpriteRenderer sr2;

    float red, green, blue; //colorのR,G,B値
    float Deletealfa = 1; //フェードアウトさせるためのa値
    float Deletespeed = 0.02f; //a値が減算されていく速さ


    private bool ShadowDelFlg;
    public bool shadowdeleteflg { set { ShadowDelFlg = value; } }
    

    // Use this for initialization
    void Start () {
        sr1 = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>(); //子オブジェクト参照
        sr2 = transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();//子オブジェクト参照

    }

    // Update is called once per frame
    //void Update ()
    void FixedUpdate()
    {
        sr1.color = new Color(red, green, blue, Deletealfa);
        sr2.color = new Color(red, green, blue, Deletealfa);
        if (ShadowDelFlg == true)
        {
            Deletealfa -= Deletespeed;

        }

    }
}
