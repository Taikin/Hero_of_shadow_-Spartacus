using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // 敵の状態
    public enum STATE
    {
        _IDLE,
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


    // 敵のポジションタイプ
    public enum ENEMYPOS_TYPE
    {
        _FORWARD,                            // 前方
        _MIDDLE,                             // 中間
        _BACK,                               // 後方
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
    [SerializeField, Header("実体の敵")]
    private GameObject entityEnemy;
    [SerializeField, Header("敵が移動するスピード")]
    private float enemySpeed;
    [SerializeField, Header("補完スピード")]
    private float completionSpeed;

    private GameObject enemyPosition;           // 敵が進んでいく位置
    private Animator animator;
    private Animator entityAnimator;            // 実体のアニメーション
    private AnimatorStateInfo aniStateInfo;
    private EnemyGenerator enemyGeneratorCon;
    private ArrowController preArrowController; // 矢のコントローラーの状態を格納する処理
    private STATE state;                        // 状態を格納
    private STATE preState;                     // 前の状態を格納
    private ENEMYTYPE enemyType;                // 敵のタイプを格納
    private ENEMYPOS_TYPE enemyPosType;         // 敵のポジションタイプを格納
    private int  randamValue;                   // ランダムな値を格納
    private float keyInputTime;                 // Time.deltaTimeの値を格納
    private float shootArrowSpeed;              // 矢を放つ時間
    private Vector3 relativePos;                // ターゲット方向のベクトルを格納
    private Quaternion rotationInformation;     // 回転情報


    public GameObject _Arrow { get { return arrow; } }
    public ENEMYTYPE _ENEMYTYPE { set { enemyType = value; } get { return enemyType; } }
    public STATE _State { set { state = value; } get { return state; } }

    // 敵生成時に参照
    public GameObject _Target { set { target = value; } }
    public GameObject _GreenPoint { set { greenPoint = value; } get { return greenPoint; } }
    public GameObject _GreenPoint1 { set { greenPoint1 = value; } get { return greenPoint1; } }
    public GameObject _EnemyPosition { get { return enemyPosition; } set{ enemyPosition = value; }}
    public EnemyGenerator _EnemyGeneratorCon { set { enemyGeneratorCon = value; } }
    public ENEMYPOS_TYPE _EnemyPos_Type { get { return enemyPosType; } set { enemyPosType = value; } }
    public float _ShootArrowSpeed { set { shootArrowSpeed = value; } }


    void Start ()
    {
        this.animator = GetComponent<Animator>();
        this.entityAnimator = entityEnemy.GetComponent<Animator>();
        this.state = STATE._IDLE;
        this.preState = STATE._IDLE;
        ArrowCreate();                                          // 矢を生成
        CheckEnemyType();                                       // 敵のタイプを初期化
    }
	
	void Update ()
    {
        EnemyAnimation();               // アニメーション処理
        EnemyState();                   // 敵の状態
        EnemyMove();                    // 敵の動き
	}

    /**********************************************************************
     * * 敵の状態
     * *******************************************************************/
    void EnemyState()
    {
        switch(state)
        {
            case STATE._IDLE:           // アイドル状態
                EnemyIdle();
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

        // 指定秒数後に矢を放つ
        if (keyInputTime >= shootArrowSpeed && state != STATE._DAMAGE)
        {
            // 矢を放つ処理へ
            state = STATE._SHOOT_BOW;
            keyInputTime = 0;
        }
        
    }

    /**********************************************************************
     * * 矢に関する処理
     * *******************************************************************/
    void EnemyShootBow()
    {
        //　現在矢を持っているなら
        if(arrow)
        {
            // 矢を放つ処理
            ShootArrow();
        }

        aniStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // 現在再生されているアニメーションが【ShootBow】だったら
        if(aniStateInfo.fullPathHash == Animator.StringToHash("Base Layer.ShootBow"))
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
        AudioManager.Instance.PlaySE("仮SE01", arrow);
        //　矢のコントローラーを取得
        var arrowController = arrow.GetComponent<ArrowController>();
        //　スタート地点を矢のスクリプトに渡す
        arrowController.CharaPos = arrowController.gameObject.transform.position;
        //　敵のポジションが一番後ろなら、greenPointを格納
        if(enemyPosType == ENEMYPOS_TYPE._BACK)
        {
            arrowController.GreenPos = greenPoint.transform.position;
        }
        // それ以外ならgreenPoint1を格納
        else if(enemyPosType == ENEMYPOS_TYPE._MIDDLE)
        {
            arrowController.GreenPos = greenPoint1.transform.position;
        }
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
        if(!arrow)
        {
            // 矢を生成する
            arrow = Instantiate(arrowPrefab);
            // 矢の位置を調整
            arrow.transform.parent = arrowPos.transform;
            arrow.transform.localPosition = Vector3.zero;
            arrow.transform.localRotation = Quaternion.identity;
            // 敵のタイプを指定
            CheckEnemyType();
        }
    }

    // 敵のタイプを調べ、それに応じて矢を生成
    void CheckEnemyType()
    {
        // 現在のレベルにあった敵のタイプにする
        enemyType = enemyGeneratorCon.CheckType();
        // 敵が一番前にいたら
        if (enemyPosType == ENEMYPOS_TYPE._FORWARD)
        {
            // 敵のタイプに応じて矢の種類を変える
            switch (enemyType)
            {
                case ENEMYTYPE._STRAOGHT:               // 直線の矢を飛ばす敵
                    RandamArrow(ArrowController.ArrowState._STRAOGHT_LINE);
                    break;
                case ENEMYTYPE._SLOW:                   // ゆっくりの矢を飛ばす敵
                    RandamArrow(ArrowController.ArrowState._SLOW_LINE);
                    break;
                case ENEMYTYPE._STRAOGHT_AND_SLOW:      // 直線とゆっくりな矢を飛ばす敵
                    RandamArrow(ArrowController.ArrowState._STRAOGHT_LINE, ArrowController.ArrowState._SLOW_LINE, 0, 2);
                    break;
                case ENEMYTYPE._ALL_ARROW:              // それ以外なら直線とゆっくりな矢を飛ばす敵にする
                    RandamArrow(ArrowController.ArrowState._STRAOGHT_LINE, ArrowController.ArrowState._SLOW_LINE, 0, 2);
                    break;
                default:                                // それ以外なら直線の矢を飛ばす敵にする
                    RandamArrow(ArrowController.ArrowState._STRAOGHT_LINE);
                    break;
            }
        }
        // それ以外なら（通常の指定された敵タイプで処理）
        else
        {
            // 敵のタイプに応じて矢の種類を変える
            switch (enemyType)
            {
                case ENEMYTYPE._STRAOGHT:               // 直線の矢を飛ばす敵
                    RandamArrow(ArrowController.ArrowState._STRAOGHT_LINE);
                    break;
                case ENEMYTYPE._CURVE:                  // 曲線の矢を飛ばす敵
                    RandamArrow(ArrowController.ArrowState._CURVE_LINE);
                    break;
                case ENEMYTYPE._SLOW:                   // ゆっくりの矢を飛ばす敵
                    RandamArrow(ArrowController.ArrowState._SLOW_LINE);
                    break;
                case ENEMYTYPE._STRAOGHT_AND_CURVE:     // 直線と曲線の矢を飛ばす敵
                    RandamArrow(ArrowController.ArrowState._STRAOGHT_LINE, ArrowController.ArrowState._CURVE_LINE, 0, 2);
                    break;
                case ENEMYTYPE._STRAOGHT_AND_SLOW:      // 直線とゆっくりな矢を飛ばす敵
                    RandamArrow(ArrowController.ArrowState._STRAOGHT_LINE, ArrowController.ArrowState._SLOW_LINE, 0, 2);
                    break;
                case ENEMYTYPE._SLOW_AND_CURVE:         // 曲線とゆっくりな矢を飛ばす敵
                    RandamArrow(ArrowController.ArrowState._CURVE_LINE, ArrowController.ArrowState._SLOW_LINE, 0, 2);
                    break;
                case ENEMYTYPE._ALL_ARROW:              // 全ての矢を飛ばす敵
                    RandamArrow(ArrowController.ArrowState._STRAOGHT_LINE, ArrowController.ArrowState._CURVE_LINE, ArrowController.ArrowState._SLOW_LINE, 3);
                    break;
            }
        }
    }

    // ランダムで矢の挙動を変える処理
    private void RandamArrow(ArrowController.ArrowState A, ArrowController.ArrowState B = 0, ArrowController.ArrowState C = 0, int value = 1)
    {
        var arrowController = arrow.GetComponent<ArrowController>();
        int randamValue = Random.Range(0, value);

        // ランダムに矢の挙動を割り振る
        switch (randamValue)
        {
            case 0:
                arrowController._ArrowState = A;
                break;
            case 1:
                arrowController._ArrowState = B;
                break;
            case 2:
                arrowController._ArrowState = C;
                break;
        }
    }

    /**********************************************************************
     * * 敵の動き処理
     * *******************************************************************/
    void EnemyMove()
    {
        // 目標地点に移動
        if (Vector3.Distance(enemyPosition.transform.position, transform.position) > 0.5f && state != STATE._DAMAGE)
        {
            Vector3 direction = (enemyPosition.transform.position - transform.position).normalized;
            transform.position += direction * Time.deltaTime * enemySpeed;
        }

        if (!arrow) { return; }

        var arrowController = arrow.GetComponent<ArrowController>();

        // 矢を放つ方向に体を傾ける
        if (arrowController._ArrowState == ArrowController.ArrowState._CURVE_LINE)
        {
            // 敵が現在いる位置に応じて体の傾け方を変える
            switch (enemyPosType)
            {
                case ENEMYPOS_TYPE._MIDDLE:
                    // ターゲット方向のベクトルを取得
                    Vector3 relativePos = greenPoint1.transform.position - transform.position;
                    // 方向を、回転情報に変換
                    rotationInformation = Quaternion.LookRotation(relativePos);
                    // 現在の回転情報と、ターゲット方向の回転情報を補完する
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotationInformation, completionSpeed);
                    break;
                case ENEMYPOS_TYPE._BACK:
                    // ターゲット方向のベクトルを取得
                    relativePos = greenPoint.transform.position - this.transform.position;
                    // 方向を、回転情報に変換
                    rotationInformation = Quaternion.LookRotation(relativePos);
                    // 現在の回転情報と、ターゲット方向の回転情報を補完する
                    transform.rotation = Quaternion.Slerp(this.transform.rotation, rotationInformation, completionSpeed);
                    break;
            }
        }
        else
        {
            // ターゲット方向のベクトルを取得
            relativePos = enemyPosition.transform.position - this.transform.position;
            // 方向を、回転情報に変換
            rotationInformation = Quaternion.LookRotation(relativePos);
            // 現在の回転情報と、ターゲット方向の回転情報を補完する
            transform.rotation = Quaternion.Slerp(this.transform.rotation, rotationInformation, completionSpeed);
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
        if(preState != state)
        {
            switch (state)
            {
                case STATE._IDLE:
                    animator.SetBool("ShootBow", false);
                    animator.SetBool("Damage",  false);
                    entityAnimator.SetBool("ShootBow", false);
                    entityAnimator.SetBool("Damage", false);
                    break;
                case STATE._SHOOT_BOW:
                    animator.SetBool("ShootBow", true);
                    entityAnimator.SetBool("ShootBow", true);
                    break;
                case STATE._DAMAGE:
                    animator.SetBool("Damage", true);
                    entityAnimator.SetBool("Damage", true);
                    break;
            }
            preState = state;
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
                return  "曲線の矢を飛ばす敵";
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

    // 敵のいるポジションの状態をセットする
    public void SetEnemyPosType(int i)
    {
        switch (i)
        {
            case 0:
                // ポジションの状態を前へ
                enemyPosType = EnemyController.ENEMYPOS_TYPE._FORWARD;
                break;
            case 1:
                // ポジションの状態を中間へ
                enemyPosType = EnemyController.ENEMYPOS_TYPE._MIDDLE;
                break;
            case 2:
                // ポジションの状態を後ろへ
                enemyPosType = EnemyController.ENEMYPOS_TYPE._BACK;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 矢に当たったら
        if (other.tag == "TestArrow" && state != STATE._DAMAGE)
        {
            AudioManager.Instance.PlaySE("仮SE02" , this.gameObject);
            // DAMAGE状態へ
            state = STATE._DAMAGE;
            // 当たった矢を削除
            Destroy(other.gameObject);
        }
    }
}
