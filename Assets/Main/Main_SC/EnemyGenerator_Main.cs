using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステータス
[System.Serializable]
public struct CREATE_LEVEL_STATUS_MAIN
{
    [Header("名前")]
    public string name;
    [Header("生成する敵のタイプ")]
    public EnemyController_Main.ENEMYTYPE type;
    [Header("生成する敵の人数")]
    public int maxCreateEnemy;
    [Header("敵の生成速度")]
    public float enemyCreateTime;
    [Header("矢を放つまでのアニメーション速度時間の最大値（1.0fの場合は常に1.0fの速度で矢を放つ）"), Range(1.0f, 5.0f)]
    public float shootArrowSpeed;
    [Header("次のLevelに移動する時間")]
    public float nextLevelTime;
};

// 生成レベルの列挙型
public enum CREATE_LEVEL_MAIN
{
    _LEVEL0,
    _LEVEL1,
    _LEVEL2,
    _LEVEL3,
    _LEVEL4,
};

public class EnemyGenerator_Main : MonoBehaviour
{
    [SerializeField, Header("カメラ")]
    private GameObject myCamera;
    [SerializeField, Header("中継地点０１")]
    private GameObject greenPoint;
    [SerializeField, Header("中継地点０２")]
    private GameObject greenPoint1;
    [SerializeField, Header("ターゲット（プレイヤー）")]
    private GameObject target;
    [SerializeField, Header("ターゲットの頭の位置")]
    private GameObject targetCurvePoint;
    [SerializeField, Header("ターゲットの影")]
    private GameObject targetShadow;
    [SerializeField, Header("敵")]
    private GameObject enemy;
    [SerializeField, Header("敵を生成する位置")]
    private GameObject enemyCreatePos;
    [SerializeField, Header("敵が進んでいく位置（目的地）")]
    private GameObject[] enemyPositions;
    [SerializeField, Header("各生成レベルにおけるステータスの設定")]
    private CREATE_LEVEL_STATUS_MAIN[] levelStatus;

    private CREATE_LEVEL_MAIN createLevel;                   // levelの値に応じて敵の処理をする
    private GameObject[] activeEnemys;                       // 現在ゲーム上にいる敵の情報
    private float comparisonTime;                            // Time.deltaTimeの値を格納(timerと比較時に使用)
    private float timer;                                     // Time.deltaTimeの値を格納

    public CREATE_LEVEL_STATUS_MAIN[] _LevelStatus { get { return levelStatus; } set { levelStatus = value; } }
    public GameObject[] _ActiveEnemys { get { return activeEnemys; } set { activeEnemys = value; } }

    private void Awake()
    {
        // 敵の情報を入れる箱
        this.activeEnemys = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            activeEnemys[i] = null;
        }
        createLevel = CREATE_LEVEL_MAIN._LEVEL0;
    }

    void Update()
    {
        //Debug.Log(createLevel);
        switch (createLevel)
        {
            case CREATE_LEVEL_MAIN._LEVEL0:
                Levels(0);          // 追加
                break;
            case CREATE_LEVEL_MAIN._LEVEL1:
                Levels(1);
                break;
            case CREATE_LEVEL_MAIN._LEVEL2:
                Levels(2);
                break;
            case CREATE_LEVEL_MAIN._LEVEL3:
                Levels(3);
                break;
            case CREATE_LEVEL_MAIN._LEVEL4:
                Levels(4);
                break;
        }

        //for(int i = 0; i < 3; i++)
        //{
        //    Debug.Log(i + 1 + "番目の敵" + activeEnemys[i]);
        //}

    }

    private void Levels(int value)
    {
        // 時間を計測し、Levelが入れ替わる時間になれば次のレベルへ
        if (IsCheckTimer(levelStatus[value].nextLevelTime)) { createLevel = ++createLevel; }
        // 指定した最大人数の敵がすでに生成されていたら、これ以降処理をしない
        if (IsMaxEnemy(levelStatus[value].maxCreateEnemy)) { return; }

        this.timer += Time.deltaTime;
        // 指定秒数後に敵を生成する
        if (levelStatus[value].enemyCreateTime <= timer)
        {
            // 敵を生成
            EnemyCreate(levelStatus[value].type, levelStatus[value].shootArrowSpeed);
            timer = 0;
        }
    }

    // 時間を計測し、次のレベルに行く時間か調べる
    private bool IsCheckTimer(float numberSecond)
    {
        // Debug.Log("現在のレベル" + createLevel + "\t" + comparisonTime);
        comparisonTime += Time.deltaTime;
        if (numberSecond < comparisonTime)
        {
            comparisonTime = 0;
            return true;
        }
        return false;
    }

    // 敵がvalueで設定された値ぶん登場しているか？
    public bool IsMaxEnemy(int value)
    {
        bool valueFlg = true;
        // 現在ゲーム上にいる敵を取得
        for (int i = 0; i < value; i++)
        {
            // 敵がvalueで設定された値ぶんいなければ、falseを返す
            if (activeEnemys[i] == null)
            {
                //   return false;
                valueFlg = false;
            }
        }
        // 敵が最大数いれば、trueを返す
        return valueFlg;
    }

    // 敵を生成する処理
    public void EnemyCreate(EnemyController_Main.ENEMYTYPE type = EnemyController_Main.ENEMYTYPE._ALL_ARROW, float _shootArrowSpeed = 1.0f)
    {
        // 敵が最大数（３体）居たら、これ以降処理をしない
        if (IsMaxEnemy(3)) { return; }
        // 敵を生成
        GameObject prefab = Instantiate(enemy, enemyCreatePos.transform.position, Quaternion.Euler(0, 90, 0));
        // 生成した敵にいくつかの情報を渡す
        var enemyController = prefab.GetComponent<EnemyController_Main>();
        enemyController._EnemyGeneratorCon = this.gameObject.GetComponent<EnemyGenerator_Main>();
        enemyController._Target = target;
        enemyController._GreenPoint = greenPoint;
        enemyController._GreenPoint1 = greenPoint1;
        enemyController._TargetShadow = targetShadow;
        enemyController._ENEMYTYPE = type;                    // 敵のタイプ格納
        enemyController._ShootArrowSpeed = _shootArrowSpeed;  // 矢を放つスピードを格納
        enemyController._TargetCurvePoint = targetCurvePoint;
        enemyController._MyCamera = myCamera;

        // 現在ゲーム上にいる敵を取得
        for (int i = 0; i < 3; i++)
        {
            // 敵がいなければ
            if (activeEnemys[i] == null)
            {
                // 現在生成した敵の情報を入れる
                activeEnemys[i] = prefab;
                // 敵が移動する目標地点を敵のスクリプトに渡す
                enemyController._EnemyPosition = enemyPositions[i];
                // 敵のポジション状態をセットする
                enemyController.SetEnemyPosType(i);
                break;
            }
        }
    }

    // 敵が死んだ時に、生きている敵の目標地点を更新する処理
    public void EnemyPosSort()
    {
        for (int i = 1; i < 3; i++)
        {
            // 生きている敵がいなければ、処理を返す
            if (activeEnemys[i] == null) { return; }
            // 敵の情報を更新
            activeEnemys[i - 1] = activeEnemys[i];
            activeEnemys[i] = null;
            var enemyController = activeEnemys[i - 1].GetComponent<EnemyController_Main>();
            // 敵の移動位置を更新
            enemyController._EnemyPosition = enemyPositions[i - 1];
            // 敵のポジション状態をセットする
            enemyController.SetEnemyPosType(i - 1);
        }
    }

    // 現在のレベルにあった敵のタイプにする
    public EnemyController_Main.ENEMYTYPE CheckType()
    {
        switch (createLevel)
        {
            case CREATE_LEVEL_MAIN._LEVEL0:
                return levelStatus[0].type;
            case CREATE_LEVEL_MAIN._LEVEL1:
                return levelStatus[1].type;
            case CREATE_LEVEL_MAIN._LEVEL2:
                return levelStatus[2].type;
            case CREATE_LEVEL_MAIN._LEVEL3:
                return levelStatus[3].type;
            case CREATE_LEVEL_MAIN._LEVEL4:
                return levelStatus[4].type;
        }

        return 0;
    }

    // 一番目の敵の位置
    public GameObject FirstEnemyPos()
    {
        GameObject firstPos = null;

        for (int i = 0; i < 3; i++)
        {
            if (activeEnemys[i])
            {
                firstPos = activeEnemys[i];
                break;

            }
        }

        return firstPos;
    }

    // 敵の動きを止める処理
    public void EnemyStop()
    {
        for (int i = 0; i < 3; i++)
        {
            if (activeEnemys[i])
            {
                var controller = activeEnemys[i].GetComponent<EnemyController_Main>();
                controller._State = EnemyController_Main.STATE._STOP;
            }
        }
    }

    // ゲーム上の敵を調べる
    public bool _CheckActiveEnemy()
    {
        for (int i = 0; i < 3; i++)
        {
            // 敵が居ればfalseを返す
            if (activeEnemys[i])
            {
                return false;
            }
        }
        return true;
    }

    // 前に敵がいるか調べる
    public bool _IsCheckForwardEnemy()
    {
        for (int i = 1; i < 3; i++)
        {
            // 前に敵が居ればtrueを返す
            if (activeEnemys[i])
            {
                return true;
            }
        }
        return false;
    }
}
