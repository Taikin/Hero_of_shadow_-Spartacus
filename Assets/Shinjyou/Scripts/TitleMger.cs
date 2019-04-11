using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleMger : MonoBehaviour {
    [SerializeField,Header("TitleImgを入れる")]
    private GameObject image;   //TitleImgを取得

    [SerializeField, Header("SceneSelectButtonをアタッチする")]
    private GameObject select;  //SceneSelectButtonを取得


    private GameObject suparutakusu;
    private Titleanim titleanim;


    void Start ()
    {
        suparutakusu = GameObject.Find("Barbarian");
    }
	

	void Update ()
    {
    }
    void OnTriggerEnter(Collider Hit)
    {
        //Debug.Log("hit");
        Destroy(suparutakusu,3.0f);     //スパルタクスと子オブジェクトを消す
        image.GetComponent<Titleanim>().enabled = true;     //[TitleImg]のオブジェクトをオンにする
        select.GetComponent<Selectanm>().enabled = true;    //[SceneSlectButton]のオブジェクトをオンにるる
    }
}