﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController_Main : MonoBehaviour {

    // 敵の状態
    public enum STATE
    {
        _IDLE,
        _MOVE,
        _SHOOT_BOW,
        _DAMAGE,

    }

    // 敵のタイプ
    public enum ENEMYTYPE
    {
        _STRAOGHT,                           // 直線の矢を飛ばす敵
        _CURVE,                              // 曲線の矢を飛ばす敵
        _SLOW,                               // ゆっくりな矢を飛ばす敵
        _STRAOGHT_AND_CURVE,                 // 直線と曲線の矢を飛ばす敵
        _STRAOGHT_AND_SLOW,                  // 直線とゆっくりな矢を飛ばす敵
        _SLOW_AND_CURVE,                     // 曲線とゆっくりな矢を飛ばす敵
        _ALL_ARROW,                          // 全ての矢を飛ばす敵
    }

    [SerializeField, Header("矢を生成する位置")]
    private GameObject arrowPos;
    [SerializeField, Header("現在持っている矢")]
    private GameObject arrow;
    [SerializeField, Header("生成する矢")]
    private GameObject arrowPrefab;
    [SerializeField, Header("中継地点０１")]
    private GameObject greenPoint;
    [SerializeField, Header("中継地点０２")]
    private GameObject greenPoint1;
    [SerializeField, Header("ターゲット（プレイヤー）")]
    private GameObject target;
    [SerializeField, Header("敵の速度")]
    private float enemySpeed;

    private GameObject enemyPosition;           // 敵が進んでいく位置
    private Animator animator;
    private AnimatorStateInfo aniStateInfo;
    private EnemyGenerator_Main enemyGeneratorCon;
    private STATE state;                        // 状態を格納
    private STATE preState;                     // 前の状態を格納
    private ENEMYTYPE enemyType;                // 敵のタイプを格納
    private float keyInputTime;                 // Time.deltaTimeの値を格納
    private float createTime;                   // 矢を生成する時間
    private int count = 0;                      // 中継地点を割り振るための変数

    public GameObject _Arrow_Main { get { return arrow; } }
    public ENEMYTYPE _ENEMYTYPE { set { enemyType = value; } get { return enemyType; } }
    public STATE _State { set { state = value; } get { return state; } }

    // 敵生成時に参照
    public GameObject _Target { set { target = value; } }
    public GameObject _GreenPoint { set { greenPoint = value; } }
    public GameObject _GreenPoint1 { set { greenPoint1 = value; } }
    public GameObject _EnemyPosition { get { return enemyPosition; } set { enemyPosition = value; } }
    public EnemyGenerator_Main _EnemyGeneratorCon { set { enemyGeneratorCon = value; } }

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.state = STATE._IDLE;
        this.preState = STATE._IDLE;
        enemyType = ENEMYTYPE._ALL_ARROW;
        ArrowCreate();
        CheckEnemyType();                                       // 敵のタイプを初期化
    }

    void Update()
    {
        EnemyAnimation();               // アニメーション処理
        EnemyState();                   // 敵の状態
        // 目標地点に移動
        if (Vector3.Distance(enemyPosition.transform.position, transform.position) > 0.1f && state != STATE._DAMAGE)
        {
            transform.position += (transform.forward * enemySpeed) * Time.deltaTime;
        }
    }

    /**********************************************************************
     * * 敵の状態
     * *******************************************************************/
    void EnemyState()
    {
        switch (state)
        {
            case STATE._IDLE:           // アイドル状態
                EnemyIdle();
                break;
            case STATE._MOVE:           // 動きの状態
                break;
            case STATE._SHOOT_BOW:      // 矢を放っている状態
                EnemyShootBow();
                break;
            case STATE._DAMAGE:         // Damageを与えられた時の処理
                EnemyDamage();
                break;
        }
    }

    /**********************************************************************
     * * アイドル状態
     * *******************************************************************/
    void EnemyIdle()
    {
        keyInputTime += Time.deltaTime;

        // 一秒後に矢を放つ
        if (keyInputTime >= 1 && state != STATE._DAMAGE)
        {
            // 矢を放つ処理へ
            state = STATE._SHOOT_BOW;
            keyInputTime = 0;
        }

    }

    /**********************************************************************
     * * 矢を放っている状態
     * *******************************************************************/
    void EnemyShootBow()
    {
        //　現在矢を持っているなら
        if (arrow)
        {
            // 矢を放つ処理
            ShootArrow();
        }

        aniStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // 現在再生されているアニメーションが【ShootBow】だったら
        if (aniStateInfo.fullPathHash == Animator.StringToHash("Base Layer.ShootBow"))
        {
            // アニメーションが終了したら、
            if (aniStateInfo.normalizedTime >= 1.0f)
            {
                // 矢を生成する
                ArrowCreate();
                // IDLE状態に戻る
                state = STATE._IDLE;
            }
        }
    }

    // 矢を放つ処理
    void ShootArrow()
    {
        //　矢のコントローラーを取得
        var arrowController = arrow.GetComponent<ArrowController_Main>();
        //　スタート地点を矢のスクリプトに渡す
        arrowController.CharaPos = arrowController.gameObject.transform.position;
        //　矢を一つ打ち出すたびに中継地点を変える
        count++;
        //　中継地点を矢のスクリプトに渡す
        if (count % 2 == 1) arrowController.GreenPos = greenPoint.transform.position;
        else arrowController.GreenPos = greenPoint1.transform.position;
        //　プレイヤー（ターゲット）の位置を矢のスクリプトに渡す
        arrowController.PlayerPos = target.transform.position;
        // 矢を飛ばす
        arrowController._IsShootFlg = true;
        // 矢を持っていない状態にする
        arrow = null;
    }

    // 矢を生成する処理
    void ArrowCreate()
    {
        // 矢が無ければ
        if (!arrow)
        {
            // 矢を生成する
            arrow = Instantiate(arrowPrefab);
            // 矢の位置を調整
            arrow.transform.parent = arrowPos.transform;
            arrow.transform.localPosition = Vector3.zero;
            arrow.transform.localRotation = Quaternion.identity;
            // 
            CheckEnemyType();
        }
    }

    // 敵のタイプを調べ、それに応じて矢を生成
    void CheckEnemyType()
    {
        //　矢のコントローラーを取得
        var arrowController_Main = arrow.GetComponent<ArrowController_Main>();

        // 敵のタイプに応じて矢の種類を変える
        switch (enemyType)
        {
            case ENEMYTYPE._STRAOGHT:               // 直線の矢を飛ばす敵
                arrowController_Main._ArrowState = ArrowController_Main.ArrowState._STRAOGHT_LINE;
                break;
            case ENEMYTYPE._CURVE:                  // 曲線の矢を飛ばす敵
                arrowController_Main._ArrowState = ArrowController_Main.ArrowState._CURVE_LINE;
                break;
            case ENEMYTYPE._SLOW:                   // ゆっくりの矢を飛ばす敵
                arrowController_Main._ArrowState = ArrowController_Main.ArrowState._SLOW_LINE;
                break;
            case ENEMYTYPE._STRAOGHT_AND_CURVE:     // 直線と曲線の矢を飛ばす敵
                arrowController_Main._ArrowState = ArrowController_Main.ArrowState._STRAOGHT_AND_CURVE_LINE;
                break;
            case ENEMYTYPE._STRAOGHT_AND_SLOW:      // 直線とゆっくりな矢を飛ばす敵
                arrowController_Main._ArrowState = ArrowController_Main.ArrowState._STRAOGHT_AND_SLOW_LINE;
                break;
            case ENEMYTYPE._SLOW_AND_CURVE:         // 曲線とゆっくりな矢を飛ばす敵
                arrowController_Main._ArrowState = ArrowController_Main.ArrowState._SLOW_AND_CURVE_LINE;
                break;
            case ENEMYTYPE._ALL_ARROW:              // 全ての矢を飛ばす敵
                arrowController_Main._ArrowState = ArrowController_Main.ArrowState._ALL_ARROW_LINE;
                break;
        }
    }

    /**********************************************************************
     * * ダメージ状態
     * *******************************************************************/
    void EnemyDamage()
    {
        aniStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // 現在再生されているアニメーションが【Damage】だったら
        if (aniStateInfo.fullPathHash == Animator.StringToHash("Base Layer.Damage"))
        {
            // アニメーションが終了したら、
            if (aniStateInfo.normalizedTime >= 1.0f)
            {
                // 敵が死んだ時に、生きている敵の目標地点を更新する処理
                enemyGeneratorCon.EnemyPosSort();
                // 敵を消す
                Destroy(this.gameObject);
            }
        }
    }

    /**********************************************************************
     * * アニメーション処理
     * *******************************************************************/
    void EnemyAnimation()
    {
        // 現在の状態と前の状態が一緒じゃなければ
        if (preState != state)
        {
            switch (state)
            {
                case STATE._IDLE:
                    animator.SetBool("ShootBow", false);
                    animator.SetBool("Damage", false);
                    break;
                case STATE._SHOOT_BOW:
                    animator.SetBool("ShootBow", true);
                    break;
                case STATE._DAMAGE:
                    animator.SetBool("Damage", true);
                    break;
            }
            preState = state;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        // 矢に当たったら
        if (other.tag == "TestArrow" && state != STATE._DAMAGE)
        {
            // DAMAGE状態へ
            state = STATE._DAMAGE;
            // 当たった矢を削除
            Destroy(other.gameObject);
        }
    }

    /**********************************************************************
     * * 外部で使用している処理
     * *******************************************************************/

    // 敵の状態に合わせてテキストを返す処理
    public string EnemyTextType()
    {
        // 敵のタイプに応じてテキストを変える
        switch (enemyType)
        {
            case ENEMYTYPE._STRAOGHT:               // 直線の矢を飛ばす敵
                return "直線に矢を飛ばす敵";
            case ENEMYTYPE._CURVE:                  // 曲線の矢を飛ばす敵
                return "曲線の矢を飛ばす敵";
            case ENEMYTYPE._SLOW:                   // ゆっくりの矢を飛ばす敵
                return "ゆっくりの矢を飛ばす敵";
            case ENEMYTYPE._STRAOGHT_AND_CURVE:     // 直線と曲線の矢を飛ばす敵
                return "直線と曲線の矢を飛ばす敵";
            case ENEMYTYPE._STRAOGHT_AND_SLOW:      // 直線とゆっくりな矢を飛ばす敵
                return "直線とゆっくりな矢を飛ばす敵";
            case ENEMYTYPE._SLOW_AND_CURVE:         // 曲線とゆっくりな矢を飛ばす敵
                return "曲線とゆっくりな矢を飛ばす敵";
            case ENEMYTYPE._ALL_ARROW:              // 全ての矢を飛ばす敵
                return " 全ての矢を飛ばす敵";
        }

        return "NULL";
    }
}
