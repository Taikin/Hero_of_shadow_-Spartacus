using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect_Main : MonoBehaviour {

    [SerializeField, Header("炎のエフェクトの部分を入れる[eff_fire]")]
    private GameObject flameEfect;
    [SerializeField, Header("一回目の炎が縮む時間")]
    private float Firsttime;
    [SerializeField, Header("二回目の炎が縮む時間")]
    private float SecondTime;
    [SerializeField, Header("炎の収縮するスピード(X軸)")]
    private float flameSizeX;
    [SerializeField, Header("炎の収縮するスピード(Y軸)")]
    private float flameSizeY;
    [SerializeField, Header("炎の収縮するスピード(Z軸)")]
    private float flameSizeZ;
    [SerializeField, Header("スパルタクス")]
    private GameObject Suparutakusu;

    private bool Firstin_Flg;
    private bool FirstOut_Flg;
    private bool Secondin_Flg;
    private bool SecondOut_Flg;
    private Vector3 Passing;
    private Vector3 One_Passing;
    private Vector3 Tow_Passing;
    private Vector3 End_Passing;
    private float QuenchingTimer;//秒数(時間)
    private bool moveone_Flg;

    Sparutakusu_Main spautakusu_main;

    void Start()
    {
        spautakusu_main = Suparutakusu.GetComponent<Sparutakusu_Main>();
        End_Passing.x = 0;
    }

    void Update()
    // void FixidUpdate()
    {
        division();

        QuenchingTimer += Time.deltaTime;
        if (QuenchingTimer >= Firsttime)
        {
            Firstin_Flg = true;
            if (One_Passing.x <= flameEfect.transform.localScale.x)
            {
                flameEfect.transform.localScale += new Vector3(-flameSizeX, -flameSizeY, -flameSizeZ);
            }
        }
        if (QuenchingTimer >= SecondTime)
        {
            Secondin_Flg = true;
            if (Tow_Passing.x <= flameEfect.transform.localScale.x)
            {
                flameEfect.transform.localScale += new Vector3(-flameSizeX, -flameSizeY, -flameSizeZ);
            }
        }
        if (spautakusu_main.TimeFlg)
        {
            if (End_Passing.x <= flameEfect.transform.localScale.x && moveone_Flg == false)
            {
                flameEfect.transform.localScale += new Vector3(-flameSizeX, -flameSizeY, -flameSizeZ);
                moveone_Flg = true;
            }
        }

    }

    void division()
    {
        if (spautakusu_main.TimeFlg == false)
        {

            if (Firstin_Flg && FirstOut_Flg == false)
            {
                Passing = flameEfect.transform.localScale;
                One_Passing = Passing;
                One_Passing.x /= 3;
                One_Passing.x *= 2;
                FirstOut_Flg = true;
            }
            if (Secondin_Flg && SecondOut_Flg == false)
            {
                Tow_Passing = Passing;
                Tow_Passing.x /= 3;
                SecondOut_Flg = true;
            }
            
        }
        //Debug.Log(spautakusu_main.TimeFlg);
        
    }
}
