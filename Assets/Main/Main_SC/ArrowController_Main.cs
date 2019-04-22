using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController_Main : MonoBehaviour {

    public enum ArrowState
    {
        _STRAOGHT_LINE,                           // 直線
        _CURVE_LINE,                              // 曲線
        _SLOW_LINE,                               // ゆっくり
    }

    [SerializeField, Header("直線に飛ぶスピード")]
    private float speed;                        // 普通の矢
    [SerializeField, Header("ゆっくり飛ぶスピード")]
    private float slowspeed;                    // ゆっくり矢
    [SerializeField, Header("曲線に飛ぶスピード")]
    private float curvespeed;                   // 曲線矢の速さ
    [SerializeField, Header("レイを飛ばす位置")]
    private GameObject RayPos;                   //レイを飛ばす位置
    [SerializeField, Header("矢の実体")]
    private GameObject Entityarrow;
    [SerializeField, Header("矢の実体02")]
    private GameObject Entityarrow02;
    [SerializeField, Header("矢を跳ね返す速さ")]
    private float Boundspeed = 5f;        //跳ね返す速さ

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
    private GameObject targetShadow;            // ターゲットの影
    private GameObject milld;
    private EntityArrowController_Main entityArrowCon;
    private EnemyGenerator_Main enemyGenerator;         // 敵を生成するスクリプト
    private GameObject enemy;

    //矢が落ち始める回転の時間
    float rotatespeed = 10f; //落ちながら回転する変数  
    float rotatespeedCurve = 5f;
    //盾(真ん中でない)場合の管理変数
    bool protect = false;
    bool middle = false;
    Vector3 direction;
    Vector3 hitposition;    //rayでhitしたオブジェクトの位置を取得
    Rigidbody2D rb;
    Ray ray;

    public bool _IsShootFlg { set { isShootFlg = value; } }
    public float _RotateSpeed { get { return rotatespeed; } }
    public float _RotateSpeedCurve { get { return rotatespeedCurve; } }
    public ArrowState _ArrowState { set { arrowState = value; } get { return arrowState; } }
    public Vector3 CharaPos { set { charaPos = value; } }
    public Vector3 PlayerPos { set { playerPos = value; } }
    public Vector3 GreenPos { set { greenPos = value; } }
    public GameObject _TargetShadow { set { targetShadow = value; } }
    public EnemyGenerator_Main _EnemyGenerator { set { enemyGenerator = value; } }

    public bool _Middle { get { return middle; } }
    public bool _Protect { get { return protect; } }

    void Start()
    {
        
        protect = false;
        middle = false ;
        entityArrowCon = Entityarrow02.GetComponent<EntityArrowController_Main>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D Arrow)
    {
        //盾（真ん中）部分に当たった時
        if (Arrow.gameObject.tag == "middlepoint" && protect == false)
        {

            DirectionArrow();
            ////真ん中に当たったフラグが立つ
            //middle = true;
            //speed = 0;
            //slowspeed = 0;
        }

        //盾(真ん中でない)部分に当たった時
        if (Arrow.gameObject.tag == "shieldpoint" && middle == false)
        {
            // 真ん中以外に当たったのでtrue
            protect = true;
            // 矢の親子関係を解除
            Entityarrow.transform.parent = null;
            speed = 0;
            slowspeed = 0;

        }
        
    }

    void DirectionArrow()
    {
        enemy = enemyGenerator.FirstEnemyPos();
        // 敵がゲーム上にいたら
        if(enemy)
        {
            // 実体の矢を入れ替え
            Entityarrow.SetActive(false);
            Entityarrow02.SetActive(true);
            // hitフラグをtrue
            entityArrowCon._Hit = true;
            //真ん中に当たったフラグが立つ
            middle = true;
            speed = 0;
            slowspeed = 0;
            if(arrowState != ArrowState._CURVE_LINE)
            {
                hitposition = enemy.transform.position + new Vector3(-1, 0.25f, 0);
                var look = (hitposition - transform.position).normalized;
                transform.rotation = Quaternion.FromToRotation(Vector2.up, look);
            }
            else
            {
                hitposition = enemy.transform.position + new Vector3(-1, 0f, 0);
                var look = (hitposition - transform.position).normalized;
                transform.rotation = Quaternion.FromToRotation(Vector2.up, look);
            }
            //transform.rotation = Quaternion.LookRotation(look, Vector2.right);
        }
        // 敵がゲーム上にいないなら
        else
        {
            // 真ん中以外に当たったのでtrue
            protect = true;
            // 矢の親子関係を解除
            Entityarrow.transform.parent = null;
            speed = 0;
            slowspeed = 0;
        }
    }

    //Raycast,Rayの処理
    void RayPlay()
    {
        //Debug.Log("OK");
        //敵の方向取得
        direction = ((transform.position) - charaPos).normalized;

        this.ray = new Ray((transform.position), -direction);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 300, Color.red);

        if (Physics.Raycast(ray, out hit, 300))
        {
            if (hit.collider.tag == "enemy")
            {
                //当たったオブジェクトの位置を取得
                hitposition = hit.point + new Vector3(-1, 0, 0);

                //当たったオブジェクトに向けて回転
                if (arrowState != ArrowState._CURVE_LINE)
                {
                    var look = (hit.point - transform.position).normalized;
                    transform.rotation = Quaternion.FromToRotation(Vector2.up, look);
                    //Quaternion LookRotation(look);
                }
                //当たったオブジェクトに向けて回転
                else
                {
                    var look = (hit.point  - transform.position).normalized;
                    transform.rotation = Quaternion.FromToRotation(Vector2.up, look);
                    //transform.Rotate(0, 0, -150f);
                }
            }
        }
    }

    void Update() {

        //direction = ((transform.position) - charaPos).normalized;

        //this.ray = new Ray((transform.position), -direction);
        //Debug.DrawRay(ray.origin, ray.direction * 300, Color.red);

        // 矢を敵が放っていたら
        if (isShootFlg && protect == false && middle == false)
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
            }
        }

        //曲線矢以外で盾（真ん中）部分に当たった時
        if (middle == true && arrowState != ArrowState._CURVE_LINE)
        {
            //if (!enemy)
            //{
            //    Destroy(gameObject);
            //} 

           // Debug.Log("直線矢" + hitposition);
            // Debug.Log("あたり");
            // float middletime = Time.deltaTime * 8;
            float middletime = Time.deltaTime * Boundspeed;

            //敵の方向へ矢が飛ぶ
            rb.MovePosition(Vector2.Lerp(this.transform.position, hitposition, middletime));
            if (Vector2.Distance(this.transform.position, hitposition) < 0.1f)
            {
                Destroy(gameObject);
            }
        }


        //盾(真ん中でない)部分に当たった時
        if (protect == true && arrowState != ArrowState._CURVE_LINE)
        {
            //rb.AddForce(new Vector2(-0.04f, 0));
            //rb.gravityScale = 0.05f;

            // 仮
            rb.AddForce(new Vector2(-0.05f, 0));
            rb.gravityScale = 0.5f;

            transform.Rotate(0, 0, rotatespeed);    //オブジェクトを回す

        }

        //曲線矢が真ん中に当たった時の処理
        if (middle == true  && arrowState == ArrowState._CURVE_LINE)
        {
            //if (!enemy)
            //{
            //    Destroy(gameObject);
            //}
            float middletime = Time.deltaTime * Boundspeed;
            //敵の方向へ矢が飛ぶ
            transform.position = Vector3.Lerp(this.transform.position, hitposition, middletime);
            if (Vector3.Distance(this.transform.position, hitposition) < 0.1f)
            {
                Destroy(gameObject);
            }
            //transform.position -= new Vector3(hitposition.x, hitposition.y ,0);

        }
        //矢がターゲットに到達したら
        else if (protect == true && arrowState == ArrowState._CURVE_LINE)
        {

            //矢を回転させながら落とす
            //rb.AddForce(new Vector2(-10, 0));
            // 仮
            rb.AddForce(new Vector2(-0.05f, -0.2f));
            //rb.gravityScale = 0.2f;
           // rb.velocity = new Vector2(-0.05f, -1.0f);
            transform.Rotate(0, 0, rotatespeedCurve);    //オブジェクトを回す
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
        //Debug.Log("曲線");
        //曲線矢なら
        if (!doOnceFlg)
        {
            ////矢を上向きに
            //look = greenPos;
            //transform.rotation = Quaternion.FromToRotation(Vector3.right, look);
            // 処理を一度だけ通すのに使用
            doOnceFlg = true;
        }
        //矢の進む割合をTime.deltaTimeで決める
        time += Time.deltaTime / curvespeed;

        if(this.transform.position != playerPos && protect == false && middle == false)
        {
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
    }

    // ゆっくり
    void SlowLine()
    {
        transform.position += transform.up * slowspeed * Time.deltaTime;
    }
}
