using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Put_out_a_Comment_Main : MonoBehaviour {

    [SerializeField, Header("コメントその1（だいたい20秒経ったら")]
    private float putComment01;
    [SerializeField, Header("コメントその２（だいたい50秒経ったら）")]
    private float putComment02;
    [SerializeField, Header("ゲージ")]
    private GameObject Gauge;

    [SerializeField, Header("コメント01")]
    private GameObject Comment01;
    [SerializeField, Header("コメント02")]
    private GameObject Comment02;

    //GameObject Comment01;
    //GameObject Comment02;
    float Gaugescollect;//ゲージのたまり具合 
    private float DestroyTimes;
    private float DestroyTimes2;
    bool PutFlg;

    //　追加
    GuageManegur_Main guagemanegur_main;

    // Use this for initialization
    void Start()
    {
        //Comment01 = GameObject.Find("Comment01");
        //Comment02 = GameObject.Find("Comment02");
        //Comment01.SetActive(false);
        //Comment02.SetActive(false);
        PutFlg = false;
        DestroyTimes = 0;
        DestroyTimes2 = 0;

        // 追加
        guagemanegur_main = Gauge.GetComponent<GuageManegur_Main>();
    }

    // Update is called once per frame

    //void Update()
     void FixedUpdate()
    {
       // Debug.Log(guagemanegur_main.Gagevalue);

        //Gaugescollect = Gauge.GetComponent<Image>().fillAmount;

        //Debug.Log(Gaugescollect);
        if (PutFlg == false)
        {
            if (/*Gaugescollect > putComment01*/ guagemanegur_main.Gagevalue >= 0.23f)
            {
                DestroyTimes += Time.deltaTime;
                if (DestroyTimes < 2f)
                {
                    Comment01.SetActive(true);
                }
                else
                {
                    Destroy(Comment01);
                    DestroyTimes = 0;
                    PutFlg = true;
                }
            }
        }

        if (PutFlg == true)
        {
            if (/*Gaugescollect > putComment02*/ guagemanegur_main.Gagevalue >= 0.56f)
            {
                DestroyTimes2 += Time.deltaTime;
                if (DestroyTimes2 < 2f)
                {
                    Comment02.SetActive(true);
                }
                else
                {
                    Destroy(Comment02);
                }

            }
        }

    }
}
