using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Glass : MonoBehaviour
{
    [SerializeField, Header("草のスピード")]
    private float GlassSpeed;
    [SerializeField, Header("新しい草を出す瞬間")]
    private float CreateGlass;
    [SerializeField, Header("古い草を消す瞬間")]
    private float DeleteGlass;

    private bool MoveFlg;
    private float LerpTime;

    public GameObject Glass;
    GameObject instanceObj;

    private bool CreateFlg;

    // Use this for initialization
    void Start()
    {
        CreateFlg = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(GlassSpeed, 0.0f, 0.0f);//x方向に動く
        if (transform.position.x < CreateGlass && CreateFlg == true)
        {
            instanceObj = Instantiate(Glass, new Vector3(5.35f, 0.85f, -9.3f), Glass.transform.rotation);//石を生やします
            CreateFlg = false;
        }
        if (transform.position.x < DeleteGlass)
        {
            Destroy(gameObject);//xが-3を超えたら消す
        }
    }
}