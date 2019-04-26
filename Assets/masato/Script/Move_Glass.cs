using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Glass : MonoBehaviour
{
    [SerializeField, Header("真ん中に止める時間")]
    private float stoptime;
    [SerializeField, Header("真ん中までに動くスピード")]
    private float speed;
    [SerializeField, Header("画面外に行く草のスピード")]
    private float　GlassSpeed;

    private GameObject ObjPos;
    private bool MoveFlg;
    private float LerpTime;
    float TimeCount;
    
    // Use this for initialization
    void Start()
    {
        TimeCount = 0;
        MoveFlg = false;
        ObjPos = GameObject.Find("ObjPos");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (MoveFlg == false)
        {

            // 草を真ん中に移動（草の位置と真ん中にあるオブジェクトの距離 > 1なら）
            if (Vector3.Distance(transform.position, ObjPos.transform.position) > 1)
            {
                LerpTime = Time.deltaTime * speed;
                transform.position = Vector3.MoveTowards(transform.position, ObjPos.transform.position, LerpTime);
            }
            //　草が真ん中に着いたら
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
            transform.position += new Vector3(GlassSpeed, 0, 0);
        }
        //TimeCount += Time.deltaTime;

        //if (TimeCount < 7.5f)
        //{
        //    flg = 0;
        //}
        //else if (TimeCount < 25f)
        //{
        //    flg = 1;
        //}
        //else
        //{
        //    flg = 2;
        //}

        //if (flg == 0 || flg == 2)
        //{
        //    transform.position += new Vector3(TC, 0, 0);
        //}
        //else if (flg == 1)
        //{
        //    transform.position = new Vector3(1.58f, 0.77f, -9.89f);
        //}


    }
}