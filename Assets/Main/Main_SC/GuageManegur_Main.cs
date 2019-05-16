using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GuageManegur_Main : MonoBehaviour {

    GameObject _Gauge;

    void Start()
    {
        // スライダーを取得する
        _Gauge = GameObject.Find("Gauge");
    }

    void FixedUpdate()
    {
        _Gauge.GetComponent<Image>().fillAmount += Time.deltaTime / 90.0f;
    }
}

