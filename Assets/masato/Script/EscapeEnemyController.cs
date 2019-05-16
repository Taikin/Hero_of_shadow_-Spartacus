using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeEnemyController : MonoBehaviour
{
    public enum STATE
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

    public GameObject clearEsapePos;
    private Animator animator;
    private Animator horseAnimator;
    private AnimatorStateInfo aniStateInfo;

    private STATE state;
    private float timer;

    public STATE _STATE { get { return state; } }

	void Start ()
    {
        state = STATE._CHASE;
        animator = GetComponent<Animator>();
        horseAnimator = horse.GetComponent<Animator>();
        
	}
	
	void Update ()
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
            transform.position = Vector3.MoveTowards(transform.position, clearEsapePos.transform.position, Time.deltaTime * 0.5f);
        }
        else
        {
            animator.SetBool("Amazed", true);       // びっくりするアニメーション再生
            horseAnimator.SetBool("Amazed", true);
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
        if(timer > 0.1f)
        {
            // efeectを交互に切り替えながら表示
            effect.SetActive(!effect.activeSelf);
            timer = 0;
        }

        transform.position += transform.forward  * Time.deltaTime;
    }
}
