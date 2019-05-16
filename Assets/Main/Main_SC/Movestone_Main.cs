using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movestone_Main : MonoBehaviour {

    [SerializeField, Header("石のスピード")]
    private float StoneSpeed;
    [SerializeField, Header("新しい石を出す瞬間")]
    private float CreateStone;
    [SerializeField, Header("古い石を消す瞬間")]
    private float DeleteStone;

    GameObject instanceObj;
    public GameObject Stone;

    private bool CreateFlg;
    private float LerpTime;
    float TimeCount;

    void Start()
    {
        CreateFlg = true;
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(StoneSpeed, 0.0f, 0.0f);//x方向に動く
        if (transform.position.x < CreateStone && CreateFlg == true)
        {
            instanceObj = Instantiate(Stone, new Vector3(2.52f, 0.85f, -9.1f), Stone.transform.rotation);//石を生やします
            CreateFlg = false;
        }
        if (transform.position.x < DeleteStone)
        {
            Destroy(gameObject);//xが-3を超えたら消す
        }
    }
}
