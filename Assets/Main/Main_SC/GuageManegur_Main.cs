using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GuageManegur_Main : MonoBehaviour {

    GameObject _Gauge;

    // 仮のゲージ量
    public float Gagevalue;

    void Start()
    {
        // スライダーを取得する
        _Gauge = GameObject.Find("Gauge");

        Gagevalue = 0.0f;
    }

    //void Update()
    void FixedUpdate()
    {
        _Gauge.GetComponent<Image>().fillAmount += Time.deltaTime / 90.0f;
        Gagevalue += Time.deltaTime / 90.0f;
    }
}

