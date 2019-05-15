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

    public GameObject Friend;
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
    {
        if (stopFlg)
        {
            enemyGeneratorCon.EnemyStop();                  // 敵の動きをストップさせる
        }
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
            if (ComeFflg == false)
            {
                Debug.Log("でた");
                caveObj = Instantiate(Friend, new Vector3(3.5f, 0.8f, -9.8f), Friend.transform.rotation);//仲間を出します
                GameObject prefab = Instantiate(esapeEnemy, new Vector3(-1.8f, 0.9f, -9.55f), Quaternion.Euler(0, 90, 0));
                EscapeEnemyController Econtroller = prefab.GetComponent<EscapeEnemyController>();
                Econtroller.clearEsapePos = clearEsapePos;
                stopFlg = true;
            }
            ComeFflg = true;
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
                if(timer > 5.0f)
                {
                    SceneManager.LoadScene("GameClear");
                    timer = 0;
                }
            }
        }
    }
}
