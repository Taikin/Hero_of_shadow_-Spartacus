using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowShild_Main : MonoBehaviour {

    private float Shild_Width, Shild_Length;
    private float Position_x, Position_y, Position_z;
    private float Rotate_x, Rotate_y, Rotate_z;
    private int Distance, Time;
    private float Horizontal, Vertical;
    private int PosFlg;
    private float ShildTime;
    // Use this for initialization
    void Start()
    {
        // 大きさ
        Shild_Width = 0.1f;
        Shild_Length = 0.1f;
        // 位置
        Position_x = -0.539f;
        Position_y = 1.8f;
        Position_z = -9.333f;
        // 回転
        Rotate_x = 0;
        Rotate_y = 0;
        Rotate_z = 0;
        // その他変数
        Distance = 0; //------- ゲームの経過時間 ----------//
        Time = 60;
        PosFlg = 0;
        ShildTime = 0; ;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(("Shild_Length")+Shild_Length);
        //Debug.Log(("Distance")+Distance);
        // 大きさ・位置・回転
        this.transform.localScale = new Vector2(Shild_Width, Shild_Length);
        this.transform.localPosition = new Vector3(Position_x, Position_y, Position_z);
        transform.eulerAngles = new Vector3(Rotate_x, Rotate_y, Rotate_z);

        Vertical = Input.GetAxis("Vertical");

        if (Time != 0 && Distance != 60)
        {
            Time--;
        }
        else if (Time == 0)
        {
            Time = 60;
            Distance += 1;
        }


        if ((Distance >= 20 && Distance <= 22))
        {
            ShildTime++;
        }
        else if ((Distance >= 50 && Distance <= 51))
        {
            ShildTime++;
        }

        if (ShildTime == 5)
        {
            ShildTime = 0;
            Shild_Length -= 0.001f;
        }

        // 盾の移位置微調整
        if (2.25f <= Position_y)
        {
            PosFlg = -1;
        }
        else if (1.7f >= Position_y)
        {
            PosFlg = 1;
        }
        else
        {
            PosFlg = 0;
        }

        // 盾の移動・回転
        if (Vertical > 0 && PosFlg != -1)
        {
            Position_y += 0.02f;
            Rotate_z -= 1.9f;
        }
        else if (Vertical < 0 && PosFlg != 1)
        {
            Position_y -= 0.02f;
            Rotate_z += 1.9f;
        }


        //if (Input.GetButton("Circle"))
        //{
        //    Position_y = 1.8f;
        //    Rotate_z = 0;
        //}
    }
}
