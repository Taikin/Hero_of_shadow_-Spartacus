using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : MonoBehaviour {

    public float Width, Length;
    public float Position_x, Position_y;
    public float Rotate_x, Rotate_y, Rotate_z;
    public int Distance, Time;
    public float Horizontal, Vertical;
    public int PosFlg;

	// Use this for initialization
	void Start () {
        // 大きさ
        Width = 1;
        Length = 1;
        // 位置
        Position_x = 0.5f;
        Position_y = 0;
        // 回転
        Rotate_x = 0;
        Rotate_y = 0;
        Rotate_z = 0;
        // その他変数
        Distance = 10; //------- ゴールまでの距離 ----------//
        Time = 60;
        PosFlg = 0;
    }
	
	// Update is called once per frame
	void Update () {
        // 大きさ・位置・回転
        this.transform.localScale = new Vector2(Width, Length);
        this.transform.localPosition = new Vector2(Position_x, Position_y);
        transform.eulerAngles = new Vector3(Rotate_x, Rotate_y, Rotate_z);

        Vertical = Input.GetAxis("Vertical");

        if (Time != 0 && Distance != 0) {
            Time--;
        }
        else if (Time == 0)
        {
            Time = 60;
            Distance -= 1;
        }

        // 盾の大きさ
        switch (Distance)
        {
            case 8:
                Length = 0.8f;
                break;

            case 5:
                Length = 0.5f;
                break;
        }

        // 盾の移位置微調整
        if (2.5f <= Position_y)
        {
            PosFlg = -1;
        }
        else if (-1 >= Position_y)
        {
            PosFlg = 1;
        }
        else
        {
            PosFlg = 0;
        }

        // 盾の移動・回転
        if (Vertical == 1 && (PosFlg == 0 || PosFlg == 1))
        {
            Position_y += 0.1f;
            Rotate_z -= 0.9f;
        }
        else if (Vertical == -1 && (PosFlg == 0 || PosFlg == -1))
        {
            Position_y -= 0.1f;
            Rotate_z += 0.9f;
        }       
    }
}
