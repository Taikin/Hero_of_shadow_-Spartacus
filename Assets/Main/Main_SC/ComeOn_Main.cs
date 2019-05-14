using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeOn_Main : MonoBehaviour
{

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
    Vector3 cameraMovePos;
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

        if (Times > 20 && ComeSflg == false)
        {
            if (ComeSflg == false)
            {
                instanceObj = Instantiate(Stone, new Vector3(2.52f, 0.85f, -9.3f), Stone.transform.rotation);//石を生やします
                //caveObj = Instantiate(Friend, new Vector3(3.5f, 0.8f, -9.8f), Friend.transform.rotation);//仲間を出します
                //cameraMovePos = caveObj.transform.position + new Vector3(0.2f, 0, 0);
                //instanceObj = Instantiate(Friend, new Vector3(1.7f, 0.8f, -9.8f), Friend.transform.rotation);//仲間を出します
            }
            ComeSflg = true;
        }
        else if (Times > 50 && ComeGflg == false)
        {
            if (ComeGflg == false)
            {
                Destroy(instanceObj);
                instanceObj = Instantiate(Glass, new Vector3(2.52f, 0.85f, -9.3f), Glass.transform.rotation);//石を出します
            }
            // ComeSflg = false;
            ComeGflg = true;
        }
        else if (Times > 85 && ComeFflg == false)
        {
            if (ComeFflg == false)
            {
                Debug.Log("でた");
                //Destroy(instanceObj);
                instanceObj = Instantiate(Friend, new Vector3(2.49f, 0.97f, -8.68f), Friend.transform.rotation);//仲間を出します
            }
            ComeFflg = true;
        }
    }
}
