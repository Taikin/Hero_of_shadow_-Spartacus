using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Sparutakusu_Main : MonoBehaviour {

    public Animator animator;
    public AudioClip DethSE;
    AudioSource audiosource;
    private float GameOverTime;
    public bool TimeFlg;

    // スパルタクスがやられる時に炎を小さくする
    private bool FireEffeFlg;
    GameObject Effect_Child;
    FireEffect_Main fireeffect_main;

    // スパルタクスがやられたときにスパルタクスもやられる
    [SerializeField, Header("スパルタクスの影がやられたとき")]
    private GameObject ShadowSparutakusu;
    ShadowSparutakusu_Main shadow_main;
   // public bool SparutakusuHitFlg;

    // Use this for initialization
    void Start()
    { 
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        animator.SetBool("is_Run", true);
        animator.SetBool("is_RoundKick", false);
        GameOverTime = 0;
        TimeFlg = false;

        shadow_main = ShadowSparutakusu.GetComponent<ShadowSparutakusu_Main>();
       // SparutakusuHitFlg = false;
    }

    // Update is called once per frame
    //void Update()
    void FixedUpdate()
    {
        if (TimeFlg == true)
        {
            GameOverTime += Time.deltaTime;
        }

        if (GameOverTime >= 1.0f)
        {
           // Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }

        if (shadow_main.ShadowHit == true)
        {
            //SparutakusuHitFlg = true;
            animator.SetBool("is_Run", false);
            animator.SetBool("is_RoundKick", true);
            audiosource.PlayOneShot(DethSE);
            Debug.Log("OK");
            TimeFlg = true;

        }
    }

    private void OnTriggerEnter(Collider Sparutakusu)
    {
        if (Sparutakusu.gameObject.tag == "Arrow")
        {
            var controller = Sparutakusu.gameObject.GetComponent<EntityArrowController_Main>();
           controller.DestroyArrow();
            //animator.SetBool("is_Run", false);
            //animator.SetBool("is_RoundKick", true);
            //audiosource.PlayOneShot(DethSE);
            //Debug.Log("OK");
            //TimeFlg = true;
        }
    }
}
