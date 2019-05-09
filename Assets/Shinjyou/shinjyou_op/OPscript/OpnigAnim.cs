using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class OpnigAnim : MonoBehaviour {
    [SerializeField,Header("ShadowSparutakusu(OP)(Image)を入れる")]
    private Image OpnigSparutakusuShadow;
    [SerializeField, Header("ExclamationMark(Image)を入れる")]
    private Image ExclamationMarkImg;
    [SerializeField, Header("charctormovementを入れる")]
    private CharctorMovement charctormovement;
    [SerializeField,Header("supalta14を入れる")]
    private GameObject playergameobject;
    [SerializeField,Header("shadowsuparuta(OP)を入れる")]
    private GameObject shadowsuparuta;
    [SerializeField,Header("Torchを入れる")]
    private GameObject torch;
    [SerializeField]
    private ParticleSystem ps;
    [SerializeField,Header("")]
    private GameObject tateshadow;

    private bool sweatImg_flg;
    public bool SweatImg_Flg       
    {
        get { return sweatImg_flg; }
        set { sweatImg_flg = value; }
    }
    //public GameObject a;

    private bool stoprotation;      //前の条件を読まないためのフラグ

    private Vector3 a;
    

    Animator animator;
    Animator shadowanimatoer;

    float b;

    //shadowparameta

    int paramator;
    int shadowparameta;

    float fmove = 90;               //

    bool flg;

    private bool Fade_Flg;  //trueになると影を○秒後に出す
    private float PlayerAppearance;
	void Start ()
    {
	}
	
	void Update ()
    {
        //Debug.Log(fmove);
        
        animator = playergameobject.GetComponent<Animator>();
        shadowanimatoer = shadowsuparuta.GetComponent<Animator>();

        shadowparameta = animator.GetInteger("shadowparameta");
        paramator = animator.GetInteger("Paramerter");
        //Debug.Log(charctormovement.Force);
        //Debug.Log(PlayerAppearance);
        if(Fade_Flg)
        {    
            PlayerAppearance += Time.deltaTime;
            if (PlayerAppearance >= 1 && stoprotation == false)      //アニメーション切り替えの時秒数で変えていく
            {
                if (fmove <= 180)
                {
                    fmove += 4f;
                    playergameobject.transform.rotation = Quaternion.Euler(0, fmove, 0);
                    shadowsuparuta.transform.rotation = Quaternion.Euler(0, fmove, 0);
                }
                shadowparameta = 3;
                paramator = 3;
                
            }
            if(PlayerAppearance >= 2)
            {
                shadowparameta = 4;
                paramator = 4;
                torch.transform.parent = GameObject.Find("Bone_L.005_end1").transform;
            }
            if(PlayerAppearance >= 3)
            {
                if (flg == false)
                {
                    torch.transform.rotation = Quaternion.Euler(-90, 0, 0);
                     torch.transform.localPosition = Vector3.zero;

                    flg = true;
                }
            }
            if(PlayerAppearance >= 3.5)
            {
                shadowparameta = 5;
            }
            if (PlayerAppearance >= 4.5)
            {
                tateshadow.SetActive(true);
                //OpnigSparutakusuShadow.enabled = true;
                var ma = ps.main;
                ma.startSize = 18;
            }
            if (PlayerAppearance >= 5 && stoprotation == false)
            {
                if (fmove <= 270)
                {
                    //Debug.Log("ok");
                    fmove += 4f;
                    playergameobject.transform.rotation = Quaternion.Euler(0, fmove, 0);
                    shadowsuparuta.transform.rotation = Quaternion.Euler(0, fmove, 0);
                }
                paramator = 5;
            }
            if (PlayerAppearance >= 5.5)
            {
                stoprotation = true;
                ExclamationMarkImg.enabled = true;
            }
            if (PlayerAppearance >= 5.7)
            {
                if(fmove <= 460)
                {
                    fmove += 12;
                    playergameobject.transform.rotation = Quaternion.Euler(0, fmove, 0);
                }
                ExclamationMarkImg.enabled = false;
                if (PlayerAppearance >= 6.3)
                {
                    sweatImg_flg = false;
                    paramator = 6;
                    charctormovement.Force = 20;       //スパルクスが走り出す(速度を入れた)
                }

            }
            if (PlayerAppearance >= 9)
            {
                //Destroy(playergameobject);
                SceneManager.LoadScene("Main");         //メインシーンに行く
            }
        }
        if (paramator == 2)
        {
            shadowparameta = 2;
            shadowanimatoer.SetInteger("shadowparameta", shadowparameta);
        }
        //Debug.Log(paramator);
        shadowanimatoer.SetInteger("shadowparameta", shadowparameta);
        animator.SetInteger("Paramerter",(paramator));

        // 追加
        if (Input.GetButtonDown("Option"))
        {
            SceneManager.LoadScene("Main");
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        sweatImg_flg = true;
        paramator = 2;
        animator.SetInteger("Paramerter", (paramator));
        charctormovement.Force = 0;             //松明を拾うため一時停止する
        
        Fade_Flg = true;
    }
}