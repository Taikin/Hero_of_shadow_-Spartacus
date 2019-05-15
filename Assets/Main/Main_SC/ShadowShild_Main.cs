using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowShild_Main : MonoBehaviour {
    public GameObject myCamera;

    private ComeOn_Main comeOnMain;
    private float Shild_Width, Shild_Length;
    private float Position_x, Position_y, Position_z;
    private float Rotate_x, Rotate_y, Rotate_z;
    private int Distance, Time;
    private float Horizontal, Vertical;
    private int PosFlg;
    private float ShildTime;
    AudioSource audiosource;
    public AudioClip ShieldDownSE;

    // Use this for initialization
    void Start()
    {
        comeOnMain = myCamera.GetComponent<ComeOn_Main>();
        audiosource = GetComponent<AudioSource>();
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
            audiosource.PlayOneShot(ShieldDownSE, 0.2F);
            ShildTime++;
        }
        else if ((Distance >= 50 && Distance <= 51))
        {
            audiosource.PlayOneShot(ShieldDownSE, 0.2F);
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

        if (comeOnMain.stopFlg) { return; }

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
    }
}
