using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public enum ArrowState
    {
        _STRAOGHT_LINE,                           // 直線
        _CURVE_LINE,                              // 曲線
        _SLOW_LINE,                               // ゆっくり
        _STRAOGHT_AND_CURVE_LINE,                 // 直線と曲線
        _STRAOGHT_AND_SLOW_LINE,                  // 直線とゆっくりな矢
        _SLOW_AND_CURVE_LINE,                     // 曲線とゆっくりな矢
        _ALL_ARROW_LINE,                          // 全ての種類の矢
    }

    [SerializeField, Header("直線に飛ぶスピード")]
    private float speed;                        // 普通の矢
    [SerializeField, Header("ゆっくり飛ぶスピード")]
    private float slowspeed;                    // ゆっくり矢
    [SerializeField, Header("曲線に飛ぶスピード")]
    private float curvespeed;                   // 曲線矢の速さ

    private ArrowState arrowState;              // 矢の状態
    private bool isShootFlg;                    // 矢を放っているか？（外部で参照して使っている【EnemyController】） 
    private bool doOnceFlg;                     // 一度だけ処理を通すためのFLg
    private bool randamFlg;
    private float time;                         // 進む割合を管理する変数
    private float step;                         // 矢が落ち始める回転の時間
    private float rotspeed = 0.8f;
    private int randamValue;                    // ランダムな値を格納
    private Vector3 look;                       // 回転する方向
    private Vector3 charaPos;                   // スタート地点
    private Vector3 playerPos;                  // ゴール地点
    private Vector3 greenPos;                   // 中継地点  

    public bool _IsShootFlg { set { isShootFlg = value; } }
    public ArrowState _ArrowState { set { arrowState = value; }  get { return arrowState; } }
    public Vector3 CharaPos { set { charaPos = value; } }
    public Vector3 PlayerPos { set { playerPos = value; } }
    public Vector3 GreenPos { set { greenPos = value; } }

    void Start()
    {
    }

	void Update ()
    {
        // 矢を敵が放っていたら
		if(isShootFlg)
        {
            // 親子関係解除
            transform.parent = null;

            //矢の状態
            switch(arrowState)
            {
                // 直線
                case ArrowState._STRAOGHT_LINE:
                    StraoghtLine();
                    break;
                // 曲線
                case ArrowState._CURVE_LINE:
                    CurveLine();
                    break;
                // ゆっくり
                case ArrowState._SLOW_LINE:
                    SlowLine();
                    break;
                // 直線と曲線
                case ArrowState._STRAOGHT_AND_CURVE_LINE:
                    if (!randamFlg) { randamValue = Random.Range(0, 2); randamFlg = true; }
                    if (randamValue == 0) { StraoghtLine(); } else { CurveLine(); }
                    break;
                // 直線とゆっくりな矢
                case ArrowState._STRAOGHT_AND_SLOW_LINE:
                    if (!randamFlg) { randamValue = Random.Range(0, 2); randamFlg = true; }
                    if (randamValue == 0) { StraoghtLine(); } else { SlowLine(); }
                    break;
                // ゆっくりと曲線
                case ArrowState._SLOW_AND_CURVE_LINE:
                    if (!randamFlg) { randamValue = Random.Range(0, 2); randamFlg = true; }
                    if (randamValue == 0) { SlowLine(); } else { CurveLine(); }
                    break;
                // 全ての種類の矢
                case ArrowState._ALL_ARROW_LINE:
                    if (!randamFlg) { randamValue = Random.Range(0, 3); randamFlg = true; }
                    if (randamValue == 0) { StraoghtLine(); }
                    else if (randamValue == 1) { CurveLine(); }
                    else { SlowLine(); }
                    break;
            }
        }
	}

    // 直線
    void StraoghtLine()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    // 曲線処理
    void CurveLine()
    {
        //曲線矢なら
        if (arrowState == ArrowState._CURVE_LINE && !doOnceFlg)
        {
            //矢を上向きに
            look = greenPos;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, look);
            // 処理を一度だけ通すのに使用
            doOnceFlg = true;
        }
        //矢の進む割合をTime.deltaTimeで決める
        time += Time.deltaTime / curvespeed;

        //二次ベジェ曲線
        //スタート地点から中継地点までのベクトル上を通る点の現在の位置
        var a = Vector3.Lerp(charaPos, greenPos, time);

        //中継地点からターゲットまでのベクトル上を通る点の現在の位置
        var b = Vector3.Lerp(greenPos, playerPos, time);

        //上の二つの点を結んだベクトル上を通る点の現在の位置（弾の位置）
        this.transform.position = Vector3.Lerp(a, b, time);             //曲線矢
                                                                        //落ちる矢の角度調整
        step = rotspeed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -120f), step); //z軸-120度まで回転させ矢を下向きに
    }

    // ゆっくり
    void SlowLine()
    {
        transform.position += transform.up * slowspeed * Time.deltaTime;
    }
}
