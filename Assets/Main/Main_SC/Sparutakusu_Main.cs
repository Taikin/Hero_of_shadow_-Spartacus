using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Sparutakusu_Main : MonoBehaviour {

    public Animator animator;
    public AudioClip DethSE;
    public AudioClip RunningSE;
    AudioSource audiosource;
    private int Time;
    bool TimeFlg;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        animator.SetBool("is_Run", true);
        animator.SetBool("is_RoundKick", false);
        audiosource.PlayOneShot(RunningSE); 
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

        if (Time >= 60)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnTriggerEnter(Collider Sparutakusu)
    {
        if (Sparutakusu.gameObject.tag == "Arrow")
        {
            animator.SetBool("is_Run", false);
            animator.SetBool("is_RoundKick", true);
            audiosource.PlayOneShot(DethSE);
            Debug.Log("OK");
            TimeFlg = true;
        }
        
    }

    //private void OnTriggerEnter2D(Collider2D Shadow)
    //{
    //    if (Shadow.gameObject.tag == "Arrow")
    //    {
    //        SceneManager.LoadScene("GameOverScene");
    //    }
    //}
}
