using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController_Main : MonoBehaviour {

    // 敵の状態
    public enum STATE
    {
        _IDLE,
        _SHOOT_BOW,
        _DAMAGE,
        _STOP,
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

    // 敵のポジションタイプ（追加）
    public enum ENEMYPOS_TYPE
    {
        _FORWARD,                            // 前方
        _MIDDLE,                             // 中間
        _BACK,                               // 後方
    }

    [SerializeField, Header("矢を生成する位置")]
    private GameObject arrowPos;
    [SerializeField, Header("曲線矢を生成する位置")]
    private GameObject arrowcCurvePos;
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
    [SerializeField, Header("補完スピード")]
    private float completionSpeed;
    [SerializeField, Header("前方に敵が居て、指定した数に一回は曲線を放つようにする")]
    private int specifiedValue;
    [SerializeField, Header("実験体の敵")]
    private GameObject entityEnemy;

    private GameObject enemyPosition;           // 敵が進んでいく位置
    private GameObject targetShadow;            // ターゲットの影
    private GameObject targetCurvePoint;
    private GameObject myCamera;
    private Animator animator;
    private AnimatorStateInfo aniStateInfo;
    private EnemyGenerator_Main enemyGeneratorCon;
    private EntityArrowController_Main entityArrow;
    private STATE state;                        // 状態を格納
    private STATE preState;                     // 前の状態を格納
    private ENEMYTYPE enemyType;                // 敵のタイプを格納
    private ENEMYPOS_TYPE enemyPosType;         // 敵のポジションタイプを格納
    private float keyInputTime;                 // Time.deltaTimeの値を格納
    private float shootArrowSpeed;              // 矢を放つ時間0
    private int arrowCheckCount;                // 矢のカウントに使用
    private bool createFlg;
    private bool curveCreateFlg;
    private Animator entityAnimator;

    public ENEMYTYPE _ENEMYTYPE { set { enemyType = value; } get { return enemyType; } }
    public STATE _State { set { state = value; } get { return state; } }

    // 敵生成時に参照
    public GameObject _Target { set { target = value; } }
    public GameObject _GreenPoint { set { greenPoint = value; } }
    public GameObject _GreenPoint1 { set { greenPoint1 = value; } }
    public GameObject _EnemyPosition { get { return enemyPosition; } set { enemyPosition = value; } }
    public GameObject _TargetShadow { set { targetShadow = value; } }
    public GameObject _TargetCurvePoint { set { targetCurvePoint = value; } }
    public GameObject _MyCamera { set { myCamera = value; } }
    public EnemyGenerator_Main _EnemyGeneratorCon { set { enemyGeneratorCon = value; } }
    public ENEMYPOS_TYPE _EnemyPos_Type { get { return enemyPosType; } set { enemyPosType = value; } }
    public float _ShootArrowSpeed { set { shootArrowSpeed = value; } }

    public AudioClip Arrowshot;
    public AudioClip HitSE;
    AudioSource audiosource;

    GameObject effect;
    private float DeleteTime = 0.001f;
    float time;
    private bool EffectFlg;
    private float HitFlg;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.entityAnimator = entityEnemy.GetComponent<Animator>();
        animator.SetFloat("ShootSpeed", shootArrowSpeed);
        entityAnimator.SetFloat("ShootSpeed", shootArrowSpeed);
        this.state = STATE._IDLE;
        this.preState = STATE._IDLE;
        enemyType = ENEMYTYPE._SLOW;
        ArrowCreate();
        CheckEnemyType();                                       // 敵のタイプを初期化
        audiosource = GetComponent<AudioSource>();

        effect = transform.GetChild(22).gameObject; //やられエフェクト
        EffectFlg = false;
        time = 0;
        HitFlg = 0;

    }

    //void Update()
    void FixedUpdate()
    {
        if (!target) { return; }
        EnemyAnimation();               // アニメーション処理
        EnemyState();                   // 敵の状態
        EnemyMove();                    // 敵の動き

        if (EffectFlg == true)
        {
            time += Time.deltaTime;
            if (DeleteTime < time)
            {
                EffectFlg = false;
                time = 0;
                HitFlg = 2;
            }
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
            case STATE._SHOOT_BOW:      // 矢を放っている状態
                EnemyShootBow();
                break;
            case STATE._DAMAGE:         // Damageを与えられた時の処理
                EnemyDamage();
                break;
            case STATE._STOP:
                break;
        }
    }

    /**********************************************************************
     * * 敵の動き
    * *******************************************************************/
    void EnemyMove()
    {

        // 目標地点に移動
        if (Vector3.Distance(enemyPosition.transform.position, transform.position) > 0.1f && state != STATE._DAMAGE)
        {
            transform.position += (transform.forward * enemySpeed) * Time.deltaTime;
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
     * * 矢を放っている状態
     * *******************************************************************/
    void EnemyShootBow()
    {
        aniStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // 現在再生されているアニメーションが【ShootBow】だったら
        if (aniStateInfo.fullPathHash == Animator.StringToHash("Base Layer.ShootBow"))
        {
            Check_ArrowShootAnimation(0.3f, 0.6f);
        }

        // 現在再生されているアニメーションが【Curve】だったら
        if (aniStateInfo.fullPathHash == Animator.StringToHash("Base Layer.Curve"))
        {
            Check_ArrowShootAnimation(0.35f, 0.6f);
        }
    }

    // 矢を放つ処理
    void ShootArrow()
    {
        //　矢のコントローラーを取得
        var ArrowController_Main_Main = arrow.GetComponent<ArrowController_Main>();
        //　スタート地点を矢のスクリプトに渡す
        ArrowController_Main_Main.CharaPos = ArrowController_Main_Main.gameObject.transform.position;
        //　敵のポジションが一番後ろなら、greenPointを格納    
        if (enemyPosType == ENEMYPOS_TYPE._BACK)
        {
            ArrowController_Main_Main.GreenPos = greenPoint.transform.position;
        }
        // それ以外ならgreenPoint1を格納
        else
        {
            ArrowController_Main_Main.GreenPos = greenPoint1.transform.position;
        }
        //　プレイヤー（ターゲット）の位置を矢のスクリプトに渡す
        ArrowController_Main_Main.PlayerPos = targetCurvePoint.transform.position;
        // 矢を飛ばす
        ArrowController_Main_Main._IsShootFlg = true;
        // 矢を持っていない状態にする
        arrow = null;
        audiosource.PlayOneShot(Arrowshot);
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
            arrow.transform.localRotation = Quaternion.identity; ;
            var ArrowController_Main = arrow.GetComponent<ArrowController_Main>();
            // ターゲットの影の情報を格納
            ArrowController_Main._TargetShadow = targetShadow;
            // 敵の生成スクリプト取得
            ArrowController_Main._EnemyGenerator = enemyGeneratorCon;
            // カメラの取得
            ArrowController_Main.myCamera = myCamera;
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
                    RandamArrow(ArrowController_Main.ArrowState._STRAOGHT_LINE);
                    break;
                case ENEMYTYPE._SLOW:                   // ゆっくりの矢を飛ばす敵
                    RandamArrow(ArrowController_Main.ArrowState._SLOW_LINE);
                    break;
                case ENEMYTYPE._STRAOGHT_AND_SLOW:      // 直線とゆっくりな矢を飛ばす敵
                    RandamArrow(ArrowController_Main.ArrowState._STRAOGHT_LINE, ArrowController_Main.ArrowState._SLOW_LINE, 0, 2);
                    break;
                case ENEMYTYPE._ALL_ARROW:              // それ以外なら直線とゆっくりな矢を飛ばす敵にする
                    RandamArrow(ArrowController_Main.ArrowState._STRAOGHT_LINE, ArrowController_Main.ArrowState._SLOW_LINE, 0, 2);
                    break;
                default:                                // それ以外なら直線の矢を飛ばす敵にする
                    RandamArrow(ArrowController_Main.ArrowState._STRAOGHT_LINE);
                    break;
            }
        }
        // それ以外なら（通常の指定された敵タイプで処理）
        else
        {
            // 敵が前方いるなら
            if (enemyGeneratorCon._IsCheckForwardEnemy())
            {
                if (RandamCurveArrow()) { return; }
            }
            // 敵のタイプに応じて矢の種類を変える
            switch (enemyType)
            {
                case ENEMYTYPE._STRAOGHT:               // 直線の矢を飛ばす敵
                    RandamArrow(ArrowController_Main.ArrowState._STRAOGHT_LINE);
                    break;
                case ENEMYTYPE._CURVE:                  // 曲線の矢を飛ばす敵
                    RandamArrow(ArrowController_Main.ArrowState._CURVE_LINE);
                    break;
                case ENEMYTYPE._SLOW:                   // ゆっくりの矢を飛ばす敵
                    RandamArrow(ArrowController_Main.ArrowState._SLOW_LINE);
                    break;
                case ENEMYTYPE._STRAOGHT_AND_CURVE:     // 直線と曲線の矢を飛ばす敵
                    RandamArrow(ArrowController_Main.ArrowState._STRAOGHT_LINE, ArrowController_Main.ArrowState._CURVE_LINE, 0, 2);
                    break;
                case ENEMYTYPE._STRAOGHT_AND_SLOW:      // 直線とゆっくりな矢を飛ばす敵
                    RandamArrow(ArrowController_Main.ArrowState._STRAOGHT_LINE, ArrowController_Main.ArrowState._SLOW_LINE, 0, 2);
                    break;
                case ENEMYTYPE._SLOW_AND_CURVE:         // 曲線とゆっくりな矢を飛ばす敵
                    RandamArrow(ArrowController_Main.ArrowState._CURVE_LINE, ArrowController_Main.ArrowState._SLOW_LINE, 0, 2);
                    break;
                case ENEMYTYPE._ALL_ARROW:              // 全ての矢を飛ばす敵
                    RandamArrow(ArrowController_Main.ArrowState._STRAOGHT_LINE, ArrowController_Main.ArrowState._CURVE_LINE, ArrowController_Main.ArrowState._SLOW_LINE, 3, true);
                    break;
            }
        }
    }

    // ランダムで矢の挙動を変える処理
    private void RandamArrow(ArrowController_Main.ArrowState A, ArrowController_Main.ArrowState B = 0, ArrowController_Main.ArrowState C = 0, int value = 1, bool probabilityFlg = false)
    {
        var ArrowController_Main_Main = arrow.GetComponent<ArrowController_Main>();
        int randamValue = Random.Range(0, value);

        if (probabilityFlg)
        {
            int rValue = Random.Range(0, 10);
            // 30%
            if (rValue >= 0 && rValue < 3)
            {
                randamValue = 0;
                // Debug.Log("30%");
            }
            // 50%
            else if (rValue >= 3 && rValue < 8)
            {
                randamValue = 1;
            }
            // 20%
            else if (rValue >= 8 && rValue < 10)
            {
                randamValue = 2;
            }
        }
        // ランダムに矢の挙動を割り振る
        switch (randamValue)
        {
            case 0:
                ArrowController_Main_Main._ArrowState = A;
                break;
            case 1:
                ArrowController_Main_Main._ArrowState = B;
                break;
            case 2:
                ArrowController_Main_Main._ArrowState = C;
                break;
        }
    }

    // 2回に一回は必ず曲線矢を放つ処理
    private bool RandamCurveArrow()
    {
        var ArrowController_Main_Main = arrow.GetComponent<ArrowController_Main>();
        int randamValue = Random.Range(0, specifiedValue);

        if (++arrowCheckCount >= specifiedValue)
        {
            ArrowController_Main_Main._ArrowState = ArrowController_Main.ArrowState._CURVE_LINE;
            arrowCheckCount = 0;
            return true;
        }

        switch (randamValue)
        {
            case 0:
                return false;
            case 1:
                return false;
            case 2:
                ArrowController_Main_Main._ArrowState = ArrowController_Main.ArrowState._CURVE_LINE;
                return true;
        }

        return false;
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
                Destroy(this.gameObject);
                enemyGeneratorCon.EnemyPosSort();
                Destroy(arrow);
                
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
                case STATE._STOP:
                    animator.SetBool("ShootBow", false);
                    animator.SetBool("Damage", false);
                    entityAnimator.SetBool("ShootBow", false);
                    entityAnimator.SetBool("Damage", false);
                    animator.SetBool("Curve", false);
                    entityAnimator.SetBool("Curve", false);
                    break;
                case STATE._SHOOT_BOW:
                    var ArrowController_Main_Main = arrow.GetComponent<ArrowController_Main>();
                    if (ArrowController_Main_Main._ArrowState == ArrowController_Main.ArrowState._CURVE_LINE)
                    {
                        // 矢の親子関係を解除
                        arrow.transform.parent = null;
                        // CurvePosを親にする
                        arrow.transform.parent = arrowcCurvePos.transform;
                        arrow.transform.localPosition = new Vector3(0, -0.027f, -0.02f);
                        arrow.transform.localRotation = Quaternion.Euler(-14, 76, 243);
                        animator.SetBool("Curve", true);
                        entityAnimator.SetBool("Curve", true);
                    }
                    else
                    {
                        animator.SetBool("ShootBow", true);
                        entityAnimator.SetBool("ShootBow", true);
                    }
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
 * * 矢を放つアニメーションの時間に応じて矢を放つタイミングを変える
 * *******************************************************************/
    void Check_ArrowShootAnimation(float _firstSpeed, float _secondSpeed)
    {
        Debug.Log("OK");
        if (aniStateInfo.normalizedTime >= _firstSpeed && !curveCreateFlg)
        {

            arrow.SetActive(true);
            curveCreateFlg = true;
        }

        if (aniStateInfo.normalizedTime >= _secondSpeed)
        {
            //　現在矢を持っているなら
            if (arrow)
            {
                // 矢を放つ処理
                ShootArrow();
            }
        }

        // アニメーションが終了したら、
        if (aniStateInfo.normalizedTime >= 1.0f)
        {
            Debug.Log("OK");
            // IDLE状態に戻る
            state = STATE._IDLE;
            ArrowCreate();
            curveCreateFlg = false;
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

    // 敵のいるポジションの状態をセットする
    public void SetEnemyPosType(int i)
    {
        switch (i)
        {
            case 0:
                // ポジションの状態を前へ
                enemyPosType = EnemyController_Main.ENEMYPOS_TYPE._FORWARD;
                break;
            case 1:
                // ポジションの状態を中間へ
                enemyPosType = EnemyController_Main.ENEMYPOS_TYPE._MIDDLE;
                break;
            case 2:
                // ポジションの状態を後ろへ
                enemyPosType = EnemyController_Main.ENEMYPOS_TYPE._BACK;
                break;
        }
    }

    void OnTriggerEnter(Collider enemy)
    {
        // ArrowImageの子オブジェクトに当たったら
        if (enemy.gameObject.tag == "Arrow" && state != STATE._DAMAGE)
        {
            var entityArrowCon = enemy.GetComponent<EntityArrowController_Main>();

            if (entityArrowCon._Hit)
            {
                entityArrow = enemy.GetComponent<EntityArrowController_Main>();
                audiosource.PlayOneShot(HitSE, 30.0F);
                entityArrow.DestroyArrow();      // 当たった矢を削除
                state = STATE._DAMAGE;
                //// 敵が死んだ時に、生きている敵の目標地点を更新する処理
                //Destroy(this.gameObject);
                //enemyGeneratorCon.EnemyPosSort();
                //Destroy(arrow);
                //entityArrowCon.DestroyArrow();      // 当たった矢を削除
                if (EffectFlg == false)
                {
                    EffectFlg = true;
                    effect.SetActive(true);
                    HitFlg = 1;
                }
            }
        }
    }

}
