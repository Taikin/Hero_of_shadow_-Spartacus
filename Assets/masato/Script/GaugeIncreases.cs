using UnityEngine;
using System.Collections;
using UnityEngine.UI; // ←※これを忘れずに入れる

public class GaugeIncreases : MonoBehaviour
{

    Slider _slider;
    void Start()
    {
        // スライダーを取得する
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
    }

    float Load_value = 0;
    void FixedUpdate()
    {
        // Load_value上昇
        Load_value += 0.1f;

        // Load_valueに値を設定
        _slider.value = Load_value;
    }
}
