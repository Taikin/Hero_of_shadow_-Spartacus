using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComeOn_Main : MonoBehaviour
{

    [SerializeField, Header("クリア時にカメラを移動させる位置")]
    private GameObject cameraMovePos;
    [SerializeField, Header("逃げる敵")]
    private GameObject esapeEnemy;
    [SerializeField, Header("クリア後に敵がプレイヤーを追い詰めるために移動する位置")]
    private GameObject clearEsapePos;
    [SerializeField, Header("RunEffect")]
    private GameObject runEffect;
    [SerializeField, Header("敵を生成するオブジェクト")]
    private GameObject enemyGenerator;

    [SerializeField, Header("仲間")]
    public GameObject Frend;
    [SerializeField, Header("洞窟")]
    public GameObject Curve;
    [SerializeField, Header("仲間の距離感")]
    private float[] distance;

    public GameObject Stone;
    public GameObject Glass;
    private Camera myCamera;

    private EnemyGenerator_Main enemyGeneratorCon;

    float Times;
    float timer;
    bool ComeFflg;//仲間のフラグ
    bool ComeSflg;//石のフラグ
    bool ComeGflg;//草のフラグ
    public bool stopFlg;

    GameObject instanceObj;
    GameObject instanceGlass;
    GameObject caveObj;
    // Vector3 cameraMovePos;
    // Use this for initialization
    void Start()
    {
        enemyGeneratorCon = enemyGenerator.GetComponent<EnemyGenerator_Main>();
        myCamera = Camera.main;
        ComeFflg = false;//仲間はいません
        ComeSflg = false;//石はありません
        ComeGflg = false;//草は生えていません
    }

    // Update is called once per frame
    void Update()
        // void FixidUpdate()
    {
       // Debug.Log(enemyGeneratorCon._CheckActiveEnemy());
        //if (stopFlg)
        //{
        //    enemyGeneratorCon.EnemyStop();                  // 敵の動きをストップさせる
        //}
            Times += Time.deltaTime;//時間を確認
                                //Times += 0.1f;

        if (Times > 20 && ComeSflg == false)
        {
            if (ComeSflg == false)
            {
                Instantiate(Stone, new Vector3(2.52f, 0.85f, -9.3f), Stone.transform.rotation);//石を生やします
            }
            ComeSflg = true;
        }
        else if (Times > 50 && ComeGflg == false)
        {
            Instantiate(Glass, new Vector3(2.52f, 0.85f, -9.15f), Glass.transform.rotation);//草を出します
            ComeGflg = true;
        }
        else if (Times > 90 && ComeFflg == false)
        {
            stopFlg = true;
            if (enemyGeneratorCon._CheckActiveEnemy())
            {
                if (ComeFflg == false)
                {
                    Debug.Log("でた");
                    GameObject prefab = Instantiate(esapeEnemy, new Vector3(-1.8f, 0.9f, -9.55f), Quaternion.Euler(0, 90, 0));
                    //caveObj = Instantiate(Friend, new Vector3(4.1f, 0.99f, -9.8f), Friend.transform.rotation);//仲間を出します
                    //GameObject friend = caveObj.transform.Find("frendPrehub").gameObject;
                    //Debug.Log(friend);
                    //var controller = friend.GetComponent<FriendController_Main>();
                    //controller._EscapeEnemy = prefab;

                    //洞窟を出します
                    caveObj = Instantiate(Curve, new Vector3(3.5f, 1.07f, -9.8f), Curve.transform.rotation);
                    for (int i = 0; i < 6; i++)
                    {
                        // 仲間を生成
                        GameObject frendPrefab = Instantiate(Frend, new Vector3(1.73f + distance[i], 0.773f, -9.383f), Frend.transform.rotation);
                        var controller = frendPrefab.GetComponent<FriendController>();
                        controller._EscapeEnemy = prefab;
                        // 一番最初の敵を止める
                        //if (i == 0) { controller._StopFlg = true; }
                    }
                    EscapeEnemyController_Main Econtroller = prefab.GetComponent<EscapeEnemyController_Main>();
                    Econtroller.clearEsapePos = clearEsapePos;
                }
                ComeFflg = true;
            }
        }

        // 洞窟が生成されたら
        if (caveObj)
        {
            //myCamera.transform.position += new Vector3(0.1f, 0.0f, 0.0f);
            transform.position = Vector3.MoveTowards(transform.position, cameraMovePos.transform.position, Time.deltaTime * 0.5f);
            if(Vector3.Distance(transform.position, cameraMovePos.transform.position) < 0.1f)
            {
                runEffect.SetActive(false);
                timer += Time.deltaTime;
                if(timer > 8.0f)
                {
                    SceneManager.LoadScene("GameClear");
                    timer = 0;
                }
            }
        }
    }
}
