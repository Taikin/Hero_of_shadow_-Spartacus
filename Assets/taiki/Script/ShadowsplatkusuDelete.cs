using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowsplatkusuDelete : MonoBehaviour {
 
    float Deleteflg;       //矢が刺さった時のフラグ
    Animator shadowanimator;
    Rigidbody2D rb2d;

    GameObject shadow_parent;   //親参照
    ShadowDeletetaikiDirector cash1;    //親スクリプト保存
	// Use this for initialization
	void Start () {
        shadowanimator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        shadow_parent = transform.parent.gameObject;    //親参照
        cash1 = shadow_parent.GetComponent<ShadowDeletetaikiDirector>();    //親のスクリプトを保存

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
        
        if (Deleteflg != 0)
        {
            cash1.DeleteFlg = Deleteflg; //値渡し
      
            //Debug.Log(Deletealfa);
        }
    }
}
