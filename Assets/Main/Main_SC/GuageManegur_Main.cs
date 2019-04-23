using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GuageManegur_Main : MonoBehaviour {

    private float Time;

    Slider _slider;
    void Start()
    {
        Time = 0;
        // スライダーを取得する
        _slider = GameObject.Find("Slider").GetComponent<Slider>();
    }

    float _hp = 0;
    void Update()
    {
        Time++;

        if (Time == 60)
        {
            _hp += 1;
            Time = 0;
        }

        // HP上昇
        //_hp += 0.01f;
        //if (_hp > 1)
        //{
        //    // 最大を超えたら0に戻す
        //    _hp = 0;
        //}

        // HPゲージに値を設定
        _slider.value = _hp / 90;

        if (_slider.value == 90)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
