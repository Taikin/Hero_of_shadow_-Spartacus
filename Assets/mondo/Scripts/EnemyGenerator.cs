using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField, Header("中継地点０１")]
    private GameObject greenPoint;
    [SerializeField, Header("中継地点０２")]
    private GameObject greenPoint1;
    [SerializeField, Header("ターゲット（プレイヤー）")]
    private GameObject target;
    [SerializeField, Header("敵")]
    private GameObject enemy;
    [SerializeField, Header("敵を生成する位置")]
    private GameObject enemyCreatePos;
    [SerializeField, Header("敵が進んでいく位置（目的地）")]
    private GameObject[] enemyPositions;

    private GameObject[] activeEnemys;      // 現在ゲーム上にいる敵の情報

    public GameObject[] _ActiveEnemys { get { return activeEnemys; } set { activeEnemys = value; } }

    private void Awake()
    {
        // 敵の情報を入れる箱
        this.activeEnemys = new GameObject[3];
    }

    void Update()
    {

    }

    // 敵が最大数（３体）登場しているか？
    public bool IsMaxEnemy()
    {
        // 現在ゲーム上にいる敵を取得
        for (int i = 0; i < 3; i++)
        {
            // 敵が最大数いなければ、falseを返す
            if (activeEnemys[i] == null)
            {
                return false;
            }
        }
        // 敵が最大数いれば、trueを返す
        return true;
    }

    // 敵を生成する処理
    public void EnemyCreate()
    {
        // 敵が最大数（３体）居たら、これ以降処理をしない
        if (IsMaxEnemy()) { return; }
        // 敵を生成
        GameObject prefab = Instantiate(enemy, enemyCreatePos.transform.position, Quaternion.Euler(0, 90, 0));
        // 生成した敵にいくつかの情報を渡す
        var enemyController = prefab.GetComponent<EnemyController>();
        enemyController._EnemyGeneratorCon = this.gameObject.GetComponent<EnemyGenerator>();
        enemyController._Target = target;
        enemyController._GreenPoint = greenPoint;
        enemyController._GreenPoint1 = greenPoint1;

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
            var enemyController = activeEnemys[i - 1].GetComponent<EnemyController>();
            // 敵の移動位置を更新
            enemyController._EnemyPosition = enemyPositions[i - 1];
            // 敵のポジション状態をセットする
            enemyController.SetEnemyPosType(i - 1);
        }
    }
}
