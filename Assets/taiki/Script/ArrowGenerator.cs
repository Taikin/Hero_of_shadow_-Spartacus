
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    //プレイヤー（ターゲット）
    public GameObject player;
    //矢のプレハブ
    public GameObject Arrow;
    //影の矢のプレハブ
    public GameObject shadow_Arrow;
    //矢を秒ごとに打ち出す
    public float targetTime;
    private float currentTime = 0;
    //中継地点1
    public GameObject greenPoint;
    //中継地点2
    public GameObject greenPoint1;

    //中継地点を割り振るための変数
    int count = 0;
    //矢の種類を管理する変数s
    public float ArrowType = 0;
    // Update is called once per frame

     void Start()
    {
       

    }
    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            ArrowType = 0;  //普通の矢に切り替え
        }

        if (Input.GetKey(KeyCode.S))
        {
            ArrowType = 1;  //ゆっくり矢に切り替え
        }
        if (Input.GetKey(KeyCode.D))
        {
            ArrowType = 2;  //曲線矢に切り替え
        }

        //矢を秒ごとに打ち出す
        currentTime += Time.deltaTime;
        if (targetTime < currentTime)
        {
            currentTime = 0;
            //敵の位置を保存
            var pos = this.gameObject.transform.position;
            //矢のプレハブを作成
            var t = Instantiate(Arrow) as GameObject;

            //影の矢プレハブを作成
            var a = Instantiate(shadow_Arrow) as GameObject;
            
            //影の矢を子にする
            a.transform.parent = t.transform;

            //矢の初期位置を敵の位置にする
            //  t.transform.position = pos;
            //矢につけているスクリプトを保存する
            var cash = t.GetComponent<TArrowController>();
            //スタート地点を矢のスクリプトに渡す
            cash.CharaPos = this.transform.position;
           
            //矢を一つ打ち出すたびに中継地点を変える
            count++;
            //中継地点を矢のスクリプトに渡す
            if (count % 2 == 1) cash.GreenPos = greenPoint.transform.position;
            else cash.GreenPos = greenPoint1.transform.position;

            
            //プレイヤー（ターゲット）の位置を矢のスクリプトに渡す
            cash.PlayerPos = player.transform.position;
            //矢の種類切り替え変数の値をスクリーンショットに渡す
            cash.Arrowtype = ArrowType;
            Debug.Log(ArrowType+"タイプ");
        }
        
    }
}