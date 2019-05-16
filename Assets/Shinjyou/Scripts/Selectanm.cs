using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Selectanm : MonoBehaviour {

    private RectTransform Startrecttransform;       //[StartImg]を入れるためのもの
    private RectTransform Endtrecttransform;        //[EndImg]  を入れるためのもの
    private float width = 300;                      //[StartImg]と[EndImg]の元のサイズ（横幅）
    private float height = 300;                     //[StartImg]と[EndImg]の元のサイズ（立幅）
    private float timer;                            //Timeを入れるもの
    private float alfa;                             //Imageの透明度
    private float changetimer;                      //シーンが切り替わるまでの時間
    private int a = 0;                              //
    private bool on_flg;                            //決定キーを押したかどうか

    public AudioClip KetteiSE;
    public AudioClip SelectSE;
    public AudioClip TitleSE;
    private AudioSource audiosource;

    [SerializeField,Header("Fade-Out(Image)を入れる")]
    private Image Fade;             //シーンが切り替わるときのフェードアウト画像
    [SerializeField,Header("StartImg(1)(Image)を入れる")]
    private Image ShadowTitle;      //スタートボタンに付ける影の画像
    [SerializeField,Header("EndImg(2)(Image)を入れる")]
    private Image ShadowEnd;        //エンドボタンに付ける影画像

    [SerializeField,Header("StartImg(Image)を入れる")]
    private Image TitleMainbutton;  //スタートに進むボタン
    [SerializeField,Header("EndImg(Image)を入れる")]
    private Image TitleEndbutton;   //エンドするボタン

    private float fadetimer;        //スタート、エンドボタンがフェードする時間
    private float buttonalfa;       //スタート、エンドボタンの透明度

    private float Vertical;

    private bool SeFlg;

    // Use this for initialization
    void Start ()
    {
        Startrecttransform = GameObject.Find("StartImg").GetComponent<RectTransform>(); //スタートボタンの情報取得
        Endtrecttransform = GameObject.Find("EndImg").GetComponent<RectTransform>();    //エンドボタンの情報取得

        audiosource = gameObject.GetComponent<AudioSource>();
        SeFlg = false;
    }

    void Update ()
    {

        Vertical = Input.GetAxis("Vertical");

        timer += Time.deltaTime;　//スタートボタンとエンドボタンの表示するまでの時間
        if (timer >= 0.5f)  //0.5秒たつと下に行く
        {
            //fadetimer += Time.deltaTime;    //
            buttonalfa += 0.2f; //0.2ずつ表示していく(最大は1)
            TitleMainbutton.GetComponent<Image>().color = new Color(1, 1, 1, buttonalfa);
            TitleEndbutton.GetComponent<Image>().color = new Color(1, 1, 1, buttonalfa);

            width -= 30f;   //初期の300から減らして大きさを元に戻していく
            height -= 30f;  //
            if (width >= 100)
            {
                Startrecttransform.sizeDelta = new Vector2(width, height);
                Endtrecttransform.sizeDelta = new Vector2(width - 30, height);   //エンドボタンは一回り小さくするため-30する
                if (SeFlg == false)
                {
                    audiosource.PlayOneShot(TitleSE);
                }
            }

            SeFlg = true;

            if (Vertical == -1 && a == 0) //(DownArrow)下ボタン
            {
                a += 1;
                audiosource.PlayOneShot(SelectSE, 2.0F);
            }
            if (Vertical == 1 && a == 1)　//(UpArrow)上ボタン
            {
                a = 0;
                audiosource.PlayOneShot(SelectSE, 2.0F);
            }
            if (Input.GetButtonDown("Circle") && a == 0)  //(Space)決定キー
            {
                on_flg = true;
                audiosource.PlayOneShot(KetteiSE, 2.0F);
            }
            else if (Input.GetButtonDown("Circle") && a == 1)　//
            {
                on_flg = true;
                audiosource.PlayOneShot(KetteiSE, 2.0F);
            }
            if(on_flg == true)//スペースキーが押されると下に行く
            {
                changetimer += Time.deltaTime;　//フェードアウトするまでシーンを切り替えるのを待つ時間
                alfa += 0.1f;
                Fade.GetComponent<Image>().color = new Color(0, 0, 0, alfa);
                if (changetimer >= 1.0f)
                {
                    if (a == 0)
                    {
                        SceneManager.LoadScene("Opning");         //メインシーンに行く
                    }
                    else
                    {
                        //SceneManager.LoadScene("end");          //エンドシーンに行く
                        //UnityEditor.EditorApplication.isPlaying = false;
                       // UnityEngine.Application.Quit();
                    }
                }
            }
            if (timer >= 0.7f)//ボタンが出てくるまで影の画像を出さない時間
            {
                if (a == 0)
                {
                    ShadowTitle.GetComponent<Image>().enabled = true;
                    ShadowEnd.GetComponent<Image>().enabled = false;
                }
                if (a == 1)
                {
                    ShadowEnd.GetComponent<Image>().enabled = true;
                    ShadowTitle.GetComponent<Image>().enabled = false;
                }
            }
            Debug.Log(a);

        }
    }
}