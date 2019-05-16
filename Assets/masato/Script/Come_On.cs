using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Come_On : MonoBehaviour
{
    [SerializeField, Header("クリア時にカメラを移動させる位置")]
    private GameObject cameraMovePos;
    [SerializeField, Header("逃げる敵")]
    private GameObject esapeEnemy;
    [SerializeField, Header("クリア後に敵がプレイヤーを追い詰めるために移動する位置")]
    private GameObject clearEsapePos;

    public GameObject Friend;
    public GameObject Stone;
    public GameObject Glass;
    private Camera myCamera;

    float Times;
    bool ComeFflg;//仲間のフラグ
    bool ComeSflg;//石のフラグ
    bool ComeGflg;//草のフラグ

    GameObject instanceObj;
    GameObject caveObj;
   // Vector3 cameraMovePos;
    // Use this for initialization
    void Start()
    {
        myCamera = Camera.main;
        ComeFflg = false;//仲間はいません
        ComeSflg = false;//石はありません
        ComeGflg = false;//草は生えていません
    }

    // Update is called once per frame
    void Update()
    {
        Times += Time.deltaTime;//時間を確認
                                //Times += 0.1f;

        if (Times > 0 && ComeSflg == false)
        {
            if (ComeSflg == false)
            {
                // 追加
                GameObject prefab = Instantiate(esapeEnemy, new Vector3(-1.8f, 0.9f, -9.55f), Quaternion.Euler(0, 90, 0));
                caveObj = Instantiate(Friend, new Vector3(3.5f, 1.07f, -9.8f), Friend.transform.rotation);//仲間を出します
                GameObject friend = caveObj.transform.Find("frendPrehub").gameObject;
                var controller = friend.GetComponent<FriendController>();
                controller._EscapeEnemy = prefab;
                // ここまで


                EscapeEnemyController Econtroller = prefab.GetComponent<EscapeEnemyController>();
                Econtroller.clearEsapePos = clearEsapePos;
            }
            ComeSflg = true;
        }
        else if (Times > 50 && ComeGflg == false)
        {
            if (ComeGflg == false)
            {
                Destroy(instanceObj);
                instanceObj = Instantiate(Glass, new Vector3(2.52f, 0.85f, -9.3f), Glass.transform.rotation);//草を出します
            }
            // ComeSflg = false;
            ComeGflg = true;
        }
        else if (Times > 90 && ComeFflg == false)
        {
            if (ComeFflg == false)
            {
                Debug.Log("でた");
                //caveObj = Instantiate(Friend, new Vector3(3.5f, 0.8f, -9.8f), Friend.transform.rotation);//仲間を出します
                //GameObject prefab = Instantiate(esapeEnemy, new Vector3(-1.8f, 0.9f, -9.55f), Quaternion.Euler(0, 90, 0));
                //EscapeEnemyController Econtroller = prefab.GetComponent<EscapeEnemyController>();
                //Econtroller.clearEsapePos = clearEsapePos;
            }
            ComeFflg = true;
        }

        // 洞窟が生成されたら
        if (caveObj)
        {
            //myCamera.transform.position += new Vector3(0.1f, 0.0f, 0.0f);
            transform.position = Vector3.MoveTowards(transform.position, cameraMovePos.transform.position,Time.deltaTime * 0.5f);
        }
    }
}
                //if (Times > 20 && Times < 50)
                //{
                //    //if (Times > 3 && Times < 6)
                //    //{

                //        if (ComeSflg == false)
                //    {
                //        instanceObj = Instantiate(Stone, new Vector3(2.52f, 0.85f, -9.3f), Stone.transform.rotation);//草を生やします

                //    }
                //    ComeSflg = true;
                //}
                //else if (Times < 85 && ComeGflg)
                //{
                //    //else if (Times < 30 && ComeGflg)
                //    //{
                //    if (ComeGflg == false)
                //    {
                //        Destroy(instanceObj);
                //        instanceObj = Instantiate(Glass, new Vector3(1.7f, 0.8f, -9.8f), Glass.transform.rotation);//石を出します

                //    }
                //    ComeGflg = true;
                //}
                //else if (Times > 85)
                //{
                ////else if ( Times > 30){
                //    if (ComeFflg == false)
                //    {
                //        Destroy(instanceObj);
                //        instanceObj = Instantiate(Friend, new Vector3(1.7f, 0.8f, -9.8f), Friend.transform.rotation);//仲間を出します
                //    }
                //    ComeFflg = true;
                //}

                //Debug.Log(Times);
            