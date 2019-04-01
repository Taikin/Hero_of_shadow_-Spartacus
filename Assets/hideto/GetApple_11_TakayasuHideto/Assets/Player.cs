using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {
    GameObject AppleRed;

    private Vector2 player;
    private float translation;

    public float Score;

    // 移動スピード
    public  float speed = 30;

    // アニメーション
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // アニメーション取得
    }

    void Clamp() {
        player = transform.position;

        player.x = Mathf.Clamp(player.x, -11, 11);
        player.y = Mathf.Clamp(player.y, -3, 3);

        transform.position = new Vector2(player.x, player.y);
	}
	
	// Update is called once per frame
	void Update () {
        Clamp();

        // 左右
        translation = Input.GetAxisRaw("Horizontal");

        // 移動するときの向き
        Vector2 direction = new Vector2(translation, 0).normalized;

        // 移動する向きとスピードを代入
        GetComponent<Rigidbody2D>().velocity = direction * speed;

        //=== アニメーション再生 ===//
        if (translation != 0)
        {
            animator.SetBool("Running", true);

            animator.SetBool("Waite02", false);
            animator.SetBool("Waite", false);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("Waite02", true);

            animator.SetBool("Running", false);
            animator.SetBool("Waite", false);
        }
        else
        {
            animator.SetBool("Waite", true);

            animator.SetBool("Waite02", false);
            animator.SetBool("Running", false);
        }

        //速さが0以下ならゲームオーバー
        if (speed <= 13)
        {
            SceneManager.LoadScene("GameOver");
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "AppleRed")
        {
            FindObjectOfType<ScoreSC>().AddPoint(20);
            speed += 2;
        }
        else if (collision.gameObject.tag == "AppleGreen")
        {
            FindObjectOfType<ScoreSC>().AddPoint(50);
        }
        else if (collision.gameObject.tag == "ApplePerple")
        {
            FindObjectOfType<ScoreSC>().AddPoint(-50);
            speed -= 3;
        }
    }
}
