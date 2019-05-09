using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Stone : MonoBehaviour {

    [SerializeField, Header("真ん中に止める時間")]
    private float stoptime;
    [SerializeField, Header("真ん中までに動くスピード")]
    private float speed;
    [SerializeField, Header("画面外に行く石のスピード")]
    private float StoneSpeed;

    private GameObject ObjPos;
    private bool MoveFlg;
    private float LerpTime;
    float TimeCount;

    void Start()
    {
        TimeCount = 0;
        MoveFlg = false;
        ObjPos = GameObject.Find("ObjPos");
    }

    void FixedUpdate()
    {

        if (MoveFlg == false)
        {

            // ストーンを真ん中に移動（ストーンの位置と真ん中にあるオブジェクトの距離 > 1なら）
            if (Vector3.Distance(transform.position, ObjPos.transform.position) > 1)
            {
                LerpTime = Time.deltaTime * speed;
                transform.position = Vector3.MoveTowards(transform.position, ObjPos.transform.position, LerpTime);
            }
            // ストーンが真ん中に着いたら
            else
            {
                TimeCount += Time.deltaTime;

                // 指定秒数後MoveFlgをTrue
                if (TimeCount > stoptime)
                {
                    MoveFlg = true;
                }
            }
        }
        else
        {
            transform.position += new Vector3(StoneSpeed, 0, 0);
        }

            //if (TimeCount < 7.5f)
            //{
            //    MoveFlg = false;
            //}
            //else if (TimeCount < 10f)
            //{
            //    MoveFlg = true;
            //}
            //else
            //{
            //   MoveFlg = false;
            //}

            //if (MoveFlg == true)
            //{
            //    transform.position = new Vector3(-0.23f, 0.84f, -9.3f);
            //}
            //else
            //{
            //    transform.position += new Vector3(TC, 0, 0);
            //}


            //if (flg == 0 || flg == 2)
            //{
            //    transform.position += new Vector3(TC, 0, 0);
            //}
            //else if (flg == 1)
            //{
            //    transform.position = new Vector3(-0.23f, 0.84f, -9.3f);
            //}

        }
}
