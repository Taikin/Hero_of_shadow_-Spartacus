using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeSC : MonoBehaviour {

    private float time = 60;

	// Use this for initialization
	void Start () {
        // 初期値の60秒
        GetComponent<Text>().text = ((int)time).ToString();
	}
	
	// Update is called once per frame
	void Update () {
        // 一秒ずつ減らす
        time -= Time.deltaTime;
        // マイナス無し
        if (time < 0) time = 0;
        GetComponent<Text>().text = ((int)time).ToString();

        // 時間が0になったらゲームオーバー
        if (time == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
