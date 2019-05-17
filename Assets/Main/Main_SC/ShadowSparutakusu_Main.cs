using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShadowSparutakusu_Main : MonoBehaviour {

    private float Shadow_Width, Shadow_Length;
    private float Shadow_x, Shadow_y, Shadow_z;
    public bool PerDecision;
    private Animator ShadowAnimation;

    // スパルタクスの影に当たった時にフェードとアウト処理
    private bool ShadowDeleteFlg;
    GameObject shadow_parent;
    ShadowSparutaDilecter_Main shadowsparutadilectoer;

    // Use this for initialization
    void Start()
    {
        ShadowAnimation = GetComponent<Animator>();

        Shadow_Width = 0.2f;
        Shadow_Length = 0.2f;
        Shadow_x = -0.23f;
        Shadow_y = 1.80f;
        Shadow_z = -9.333f;
        shadow_parent = transform.parent.gameObject;
        shadowsparutadilectoer = shadow_parent.GetComponent<ShadowSparutaDilecter_Main>();

        PerDecision = false;
    }

    // Update is called once per frame
    void Update()
    // void FixidUpdate()
    {
        this.transform.localScale = new Vector2(Shadow_Width, Shadow_Length);
        //this.transform.localPosition = new Vector3(Shadow_x, Shadow_y, Shadow_z);

        if (ShadowDeleteFlg == true)
        {
            shadowsparutadilectoer.shadowdeleteflg = ShadowDeleteFlg;
        }
    }

    private void OnTriggerEnter2D(Collider2D Shadow)
    {
        if (Shadow.gameObject.tag == "Arrow")
        {
          //  Debug.Log("ゲームオーバー");
            PerDecision = true;
            ShadowDeleteFlg = true;
            ShadowAnimation.SetBool("Delete", true);
        }
    }
}
