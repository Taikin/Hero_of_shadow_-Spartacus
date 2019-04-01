using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSC : MonoBehaviour {

    // スコア表示
    public Text scoreText;

    // ハイスコアを表示
    public Text highScoreText;

    // スコア
    private int score;

    // ハイスコア
    private int highScore;

    // Playerで保存するためのキー
    private string highScoreKey = "highScore";

	// Use this for initialization
	void Start () {
        Initialize();
    }
	
	// Update is called once per frame
	void Update () {
        // スコアがハイスコアより大きければ
        //if (highScore < score)
        //{
            highScore = score;
        //}

        // スコアハイスコアを表示する
        scoreText.text = score.ToString();
        highScoreText.text = highScore.ToString();
	}

    // ゲーム開始前の初期状態に戻る
    private void Initialize()
    {
        // スコアを0に戻す
        score = 0;

        // ハイスコアを取得する。保存されていなければ0を取得
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
    }

    // ポイントの追加
    public void AddPoint(int point)
    {
        score = score + point;
    }

    // ハイスコアの保存
    public void Save()
    {
        // ハイスコアを保存
        PlayerPrefs.SetInt(highScoreKey, highScore);
        PlayerPrefs.Save();

        // ゲーム開始前の状態に戻る
        Initialize();
    }
}
