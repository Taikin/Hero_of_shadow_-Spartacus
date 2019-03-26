using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowController : MonoBehaviour {
   
    //それぞれの位置を保存する変数
    //スタート地点
    private Vector3 charaPos;
    public Vector3 CharaPos { set { charaPos = value; } }
    //ゴール地点
    private Vector3 playerPos;
    public Vector3 PlayerPos { set { playerPos = value; } }
    //中継地点
    private Vector3 greenPos;
    public Vector3 GreenPos { set { greenPos = value; } }
    
    //矢の種類
    private float arrowtype;
    public float Arrowtype { set { arrowtype = value; } }

    //進む割合を管理する変数
    float time;

    public float speed;             //普通の矢
    public float slowspeed;  //ゆっくり矢
    public float curvespeed;        //曲線矢の速さ

    //上に力を入れる変数
    float gravity = 1;

    //矢が落ち始める回転の時間
    float step;
    float rotspeed=0.8f;

    //盾(真ん中でない)場合の管理変数
    bool protect= false;

    
    //回転する方向
    Vector3 look;
    Rigidbody rb;
    
    Collider child;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
      //  child = transform.FindChild("Arrow00_mesh(Clone)").gameObject.GetComponent<BoxCollider>(); ;
        
        //曲線矢なら
        if (arrowtype == 2)
        {

            //矢を上向きに
            look = greenPos;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, look);
        }
        
        
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
            Debug.Log("aa");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        //盾(真ん中でない)部分に当たった時
        if (protect == true)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionZ;
            //if (arrowtype == 2)
            //{
              //  child.isTrigger = true;
           // }
            rb.useGravity = true;
          //  Debug.Log("2");
            
        }

        if (arrowtype==0)
        {
            /*普通の矢*/
            rb.AddForce(speed, gravity, 0);
        }

         if (arrowtype==1)
        {
          
             /*ゆっくりの矢*/
            rb.AddForce(slowspeed, 0, 0);
        }
       
        /*************曲線矢*************/
        if(arrowtype==2)
        {

            //矢の進む割合をTime.deltaTimeで決める
            time += Time.deltaTime / curvespeed;

            //ターゲットまで到達していないとき  
            if (this.transform.position != playerPos&& protect== false)
            {
                //二次ベジェ曲線
                //スタート地点から中継地点までのベクトル上を通る点の現在の位置
               var a = Vector3.Lerp(charaPos, greenPos, time);

                //中継地点からターゲットまでのベクトル上を通る点の現在の位置
               var b = Vector3.Lerp(greenPos, playerPos, time);
                //上の二つの点を結んだベクトル上を通る点の現在の位置（弾の位置）
                this.transform.position = Vector3.Lerp(a, b, time);             //曲線矢

                step = rotspeed * Time.deltaTime;   //落ちる矢の角度調整
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -120f), step); //z軸-120度まで回転させ矢を下向きに
               
            }
            //矢がターゲットに到達したら
            if (protect == true)
            {
                //矢を回転させながら落とす
                rb.AddForce(-3, 0, 0);
                transform.Rotate(0, 0, 2);
            }
            
        }
        
    }

}
