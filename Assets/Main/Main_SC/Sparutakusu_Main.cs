using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Sparutakusu_Main : MonoBehaviour {

    public Animator animator;
    private int Time;
    bool TimeFlg;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("is_Run", true);
        Time = 0;
        TimeFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeFlg == true)
        {
            Time++;
        }

        if (Time >= 250)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOverScene");
        }
    }
    private void OnTriggerEnter2D(Collider2D Shadow)
    {
        if (Shadow.gameObject.tag == "Arrow")
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
