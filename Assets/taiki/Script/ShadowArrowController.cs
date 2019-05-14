﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowArrowController : MonoBehaviour {

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
    public float slowspeed;          //ゆっくり矢
    public float curvespeed;        //曲線矢の速さ

    //矢が落ち始める回転の時間
    float step;
    float rotspeed = 0.8f;
    float Boundspeed = 5f;        //跳ね返す速さ
    float rotatespeed = 2f; //落ちながら回転する変数   
    bool protect = false;  //盾(真ん中でない)場合の管理変数

    bool middle = false;
    Vector3 direction;

    GameObject modelobj;    //実体の矢
    GameObject windobj;     //風エフェクト

    Vector3 hitposition;    //rayでhitしたオブジェクトの位置を取得

    //回転する方向
    Vector3 look;
    Rigidbody rb;
    Ray ray;
    Vector3 HitEffect;
    AudioSource audiosource;
    public AudioClip Middlesound;   //真ん中に当たった時の矢の音
    public AudioClip Arrowshot;     //敵が放った矢の音

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

        //風のエフェクトオブジェクト取得
    //    windobj = transform.GetChild(0).gameObject;
        //子の実体矢オブジェクト取得
        modelobj = transform.GetChild(1).gameObject;

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
            Debug.Log("あたり");
            protect = true;
            speed = 0;
            slowspeed = 0;



        }
        //盾（真ん中）部分に当たった時
        if (collision.gameObject.tag == "middlepoint")
        {

            //曲線矢以外なら
            if (arrowtype != 2)
            {
                //実体矢の親子関係一旦解除
                modelobj.transform.parent = null;
            }

            Debug.Log("真ん中あたり");

            audiosource.PlayOneShot(Middlesound);   //真ん中に当たった時の音再生
            middle = true;   //真ん中に当たったフラグが立つ
            speed = 0;
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
        //跳ね返ったフラグが立っていて敵に当たった時
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
                //Debug.Log("teki");
                // Debug.Log(hitposition + "位置");

                //当たったオブジェクトに向けて回転
                if (arrowtype != 2)
                {

                    Vector2 look = hit.point;
                    transform.rotation = Quaternion.FromToRotation(Vector2.right + new Vector2(0, 1.0f), look);

                }
                //当たったオブジェクトに向けて回転
                else
                {
                    transform.Rotate(0, 0, -150f);
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {

        //曲線矢以外で盾（真ん中）部分に当たった時
        if (middle == true && arrowtype != 2)
        {

            //再び実体の矢を子にする
            modelobj.transform.parent = this.transform;
            //実体の矢を左向きに回転
            modelobj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90f);

            //風エフェクトを表示
          //  windobj.SetActive(true);
            // Debug.Log("あたり");
            float middletime = Time.deltaTime * 8;
            //敵の方向へ矢が飛ぶ
            rb.MovePosition(Vector2.Lerp(this.transform.position, hitposition, middletime));

        }


        //盾(真ん中でない)部分に当たった時
        if (protect == true && arrowtype != 2)
        {
            ////再び実体の矢を子にする
            modelobj.transform.parent = this.transform;
            ////実体の矢を左向きに回転
            modelobj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90f);

            rb.AddForce(new Vector2(-1f, 0));
            rb.useGravity =true;
            transform.Rotate(0, 0, rotatespeed);    //オブジェクトを回す

        }

        if (arrowtype == 0)
        {
            /*普通の矢*/
            rb.AddForce(new Vector2(speed, 0));

        }

        else if (arrowtype == 1)
        {

            /*ゆっくりの矢*/
            rb.AddForce(new Vector2(slowspeed, 0));

        }

        /*************曲線矢*************/
        else if (arrowtype == 2)
        {

            //実体矢のy軸を調整
            Vector3 pos = modelobj.transform.position;
            pos.y = 30;  //実体の敵に合わせてよい 
            modelobj.transform.position = pos;


            //矢の進む割合をTime.deltaTimeで決める
            time += Time.deltaTime / curvespeed;

            //ターゲットまで到達していないとき  
            if (this.transform.position != playerPos && protect == false && middle == false)
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
            //曲線矢が真ん中に当たった時の処理
            else if (middle == true)
            {
                // Debug.Log("あたり");
                float middletime = Time.deltaTime * Boundspeed;
                //敵の方向へ矢が飛ぶ
                transform.position = Vector3.Lerp(this.transform.position, hitposition, middletime);

            }
            //矢がターゲットに到達したら
            else if (protect == true)
            {


                //矢を回転させながら落とす
                rb.AddForce(new Vector2(-3, 0));
                rb.useGravity = true;
                transform.Rotate(0, 0, rotatespeed);    //オブジェクトを回す
            }

        }

    }

}