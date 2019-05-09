using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GaugeIncreases : MonoBehaviour
{

    GameObject _Gauge;

    void Start()
    {
        // スライダーを取得する
        _Gauge = GameObject.Find("Gauge");
    }

    void FixedUpdate()
    {
        _Gauge.GetComponent<Image>().fillAmount += Time.deltaTime*0.01f;
    }
}
