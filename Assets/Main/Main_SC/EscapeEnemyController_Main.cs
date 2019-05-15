using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeEnemyController_Main : MonoBehaviour
{

    enum STATE
    {
        _CHASE,
        _AMAZED,
        _ESCAPE,
    };

    [SerializeField, Header("汗のエフェクト")]
    private GameObject effect;
    [SerializeField, Header("乗っている馬")]
    private GameObject horse;
    [SerializeField, Header("びっくり画像")]
    private GameObject SurprisedImg;
    [SerializeField, Header("影の馬")]
    private GameObject Kageuma;
    [SerializeField, Header("影の敵")]
    private GameObject Kageteki;
    [SerializeField, Header("影の敵02")]
    private GameObject Kageteki02;

    public GameObject clearEsapePos;
    private Animator animator;
    private Animator horseAnimator;
    private Animator KageumaAnimator;
    private Animator KagetekiAnimator;
    private AnimatorStateInfo aniStateInfo;

    private STATE state;
    private float timer;

    public AudioClip HorseSE;
    AudioSource audiosource;


    void Start()
    {
        state = STATE._CHASE;
        animator = GetComponent<Animator>();
        KageumaAnimator = Kageuma.GetComponent<Animator>();
        KagetekiAnimator = Kageteki.GetComponent<Animator>();
        horseAnimator = horse.GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
    }

    void Update()
    {
        switch (state)
        {
            case STATE._CHASE:
                ChaseMove();
                break;
            case STATE._AMAZED:
                AmazedMove();
                break;
            case STATE._ESCAPE:
                EscapeMove();
                break;
        }

    }

    // ターゲットを追いかける
    void ChaseMove()
    {
        if (Vector3.Distance(transform.position, clearEsapePos.transform.position) > 0.1)
        {
            Debug.Log("OK");
            transform.position = Vector3.MoveTowards(transform.position, clearEsapePos.transform.position, Time.deltaTime * 0.5f);
        }
        else
        {
            audiosource.PlayOneShot(HorseSE);
            animator.SetBool("Amazed", true);       // びっくりするアニメーション再生
            horseAnimator.SetBool("Amazed", true);
            KageumaAnimator.SetBool("Amazed", true);       // びっくりするアニメーション再生
            KagetekiAnimator.SetBool("Amazed", true);
            SurprisedImg.SetActive(true);           // びっくりする画像表示
            state = STATE._AMAZED;                  // びっくりする処理へ
        }
    }

    // 驚く
    void AmazedMove()
    {
        aniStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // 現在再生されているアニメーションが【ShootBow】だったら
        if (aniStateInfo.fullPathHash == Animator.StringToHash("Base Layer.Amazed"))
        {
            // アニメーションが終了したら、
            if (aniStateInfo.normalizedTime >= 1.0f)
            {
                Kageteki.SetActive(false);
                Kageteki02.SetActive(true);
                horseAnimator.SetBool("Amazed", false);
                state = STATE._ESCAPE;          // 逃げる状態へ
                transform.rotation = Quaternion.Euler(0, -90, 0);
                SurprisedImg.SetActive(false);           // びっくりする画像非表示
                timer = 0;
            }
            //animator.SetBool("Amazed", false);
        }
        //timer += Time.deltaTime;

        //if(timer > 1.0f)
        //{
        //    state = STATE._ESCAPE;          // 逃げる状態へ
        //    transform.rotation = Quaternion.Euler(0, -90, 0);
        //    SurprisedImg.SetActive(false);           // びっくりする画像非表示
        //    timer = 0;
        //}
    }

    // ターゲットの仲間から逃げる
    void EscapeMove()
    {
        timer += Time.deltaTime;

        // エフェクト表示
        if (timer > 0.1f)
        {
            // efeectを交互に切り替えながら表示
            effect.SetActive(!effect.activeSelf);
            timer = 0;
        }

        transform.position += transform.forward * Time.deltaTime;
    }
}
