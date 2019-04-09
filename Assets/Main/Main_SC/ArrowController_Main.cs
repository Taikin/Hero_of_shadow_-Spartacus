using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController_Main : MonoBehaviour {

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
    public ArrowState _ArrowState { set { arrowState = value; } get { return arrowState; } }
    public Vector3 CharaPos { set { charaPos = value; } }
    public Vector3 PlayerPos { set { playerPos = value; } }
    public Vector3 GreenPos { set { greenPos = value; } }





    //上に力を入れる変数
    float gravity = 1;

    //矢が落ち始める回転の時間
    float Boundspeed = 5f;        //跳ね返す速さ
    float rotatespeed = 2f; //落ちながら回転する変数  
    
    //盾(真ん中でない)場合の管理変数
    bool protect = false;

    bool middle = false;
    Vector3 direction;

    Vector3 hitposition;    //rayでhitしたオブジェクトの位置を取得

    Rigidbody rb;
    Ray ray;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //盾(真ん中でない)部分に当たった時
        if (collision.gameObject.tag == "shieldpoint")
        {

            protect = true;
            speed = 0;
            gravity = 0;
            slowspeed = 0;

        }
        //盾（真ん中）部分に当たった時
        if (collision.gameObject.tag == "middlepoint")
        {
            //真ん中に当たったフラグが立つ
            middle = true;
            speed = 0;
            gravity = 0;
            slowspeed = 0;
            RayPlay();  //Raycast,Rayの関数呼び出し


        }

        //矢が地面に当たると消す
        if (collision.gameObject.tag == "ground")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //跳ね返ったフラグが立っていてgameObjectのタグに当たった時
        if (other.gameObject.tag == "enemy" && middle == true)
        {
            Destroy(this.gameObject);
        }

    }

    //Raycast,Rayの処理
    void RayPlay()
    {

        //敵の方向取得
        direction = (this.transform.position - charaPos).normalized;

        this.ray = new Ray(transform.position, -direction);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 300, Color.red);

        if (Physics.Raycast(ray, out hit, 300))
        {
            if (hit.collider.tag == "enemy")
            {
                //当たったオブジェクトの位置を取得
                hitposition = hit.point + new Vector3(-1, 0, 0);
                Debug.Log("teki");
                Debug.Log(hitposition + "位置");

                //当たったオブジェクトに向けて回転
                if (arrowState != ArrowState._CURVE_LINE)
                {
                    Vector2 look = hit.point;
                    transform.rotation = Quaternion.FromToRotation(Vector2.right + new Vector2(0, 1.4f), look);

                }
                //当たったオブジェクトに向けて回転
                else
                {
                    transform.Rotate(0, 0, -150f);
                }
            }
        }


    }











    void Update()
    {
        // 矢を敵が放っていたら
        if (isShootFlg)
        {
            // 親子関係解除
            transform.parent = null;

            //矢の状態
            switch (arrowState)
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
       // Debug.Log("直線");
        transform.position += transform.up * speed * Time.deltaTime;
    }

    // 曲線処理
    void CurveLine()
    {
        //Debug.Log("曲線");
        //曲線矢なら
        if (!doOnceFlg)
        {
            //矢を上向きに
            look = greenPos;
            transform.rotation = Quaternion.FromToRotation(Vector3.left, look);
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
       // Debug.Log("ゆっくり ");
        transform.position += transform.up * slowspeed * Time.deltaTime;
    }
}
