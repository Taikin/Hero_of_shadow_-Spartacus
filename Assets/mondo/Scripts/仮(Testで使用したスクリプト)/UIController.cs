using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField, Header("テストの矢")]
    private GameObject testArrowPrefab;
    [SerializeField, Header("テストの矢を生成する位置")]
    private GameObject testArrowCreatePos;
    [SerializeField, Header("敵を生成するオブジェクト")]
    private GameObject enemyGenerator;
    [SerializeField, Header("敵の情報を記す")]
    private GameObject enemysText;

    private EnemyGenerator enemyGeneratorCon;
    private Text mytext;

    private void Start()
    {
        this.enemyGeneratorCon = enemyGenerator.GetComponent<EnemyGenerator>();
        mytext = enemysText.GetComponent<Text>();
    }

    private void Update()
    {
        // 敵の情報を書き込むテキスト（処理が重くなるかも）
        EnemyInformationText();
    }

    // 敵の情報を書き込むテキスト
    public void EnemyInformationText()
    {
        mytext.text = "";
        GameObject[] prefabs = enemyGeneratorCon._ActiveEnemys;

        for (int i = 0; i < 3; i++)
        {
            // 敵がいたら
            if (prefabs[i])
            {
                var enemyCon = prefabs[i].GetComponent<EnemyController>();
                // 敵の情報を書き込む
                mytext.text += i + 1 + "番目の敵のタイプ:" + enemyCon.EnemyTextType() + "\n";
            }
        }
    }

    // 敵に矢を飛ばすUI
    public void OnClick_Skip_The_Arrow()
    {
        // 矢を生成
        Instantiate(testArrowPrefab, testArrowCreatePos.transform.position, Quaternion.Euler(0, 0, 90));
    }

    // 矢を生成するUI
    public void OnClick_EnemyCreate()
    {
        // 敵を生成
        enemyGeneratorCon.EnemyCreate();
    }

    /**********************************************************************
     * * 敵のタイプを変える処理
     * *******************************************************************/

    // 敵のタイプを変える処理
    private void EnemyTypeChange(EnemyController.ENEMYTYPE type)
    {
        GameObject[] prefabs = enemyGeneratorCon._ActiveEnemys;

        for (int i = 0; i < 3; i++)
        {
            if (prefabs[i])
            {
                var enemyCon = prefabs[i].GetComponent<EnemyController>();
                enemyCon._ENEMYTYPE = type;
            }
        }
        // 敵の情報を書き込むテキスト
        EnemyInformationText();
    }

    // 直線の矢を飛ばす敵に変更
    public void OnClick_STRAOGHT()
    {
        EnemyTypeChange(EnemyController.ENEMYTYPE._STRAOGHT);
    }

    // 曲線の矢を飛ばす敵に変更
    public void OnClick_CURVE()
    {
        EnemyTypeChange(EnemyController.ENEMYTYPE._CURVE);
    }

    // ゆっくりの矢を飛ばす敵に変更
    public void OnClick_SLOW()
    {
        EnemyTypeChange(EnemyController.ENEMYTYPE._SLOW);
    }

    // 直線と曲線の矢を飛ばす敵に変更
    public void OnClick_STRAOGHT_AND_CURVE()
    {
        EnemyTypeChange(EnemyController.ENEMYTYPE._STRAOGHT_AND_CURVE);
    }

    // 直線とゆっくりな矢を飛ばす敵に変更
    public void OnClick_STRAOGHT_AND_SLOW()
    {
        EnemyTypeChange(EnemyController.ENEMYTYPE._STRAOGHT_AND_SLOW);
    }

    // 曲線とゆっくりな矢を飛ばす敵に変更
    public void OnClick_SLOW_AND_CURVE()
    {
        EnemyTypeChange(EnemyController.ENEMYTYPE._SLOW_AND_CURVE);
    }

    // 全ての矢を飛ばす敵に変更
    public void OnClick_ALL_ARROW()
    {
        EnemyTypeChange(EnemyController.ENEMYTYPE._ALL_ARROW);
    }
}
