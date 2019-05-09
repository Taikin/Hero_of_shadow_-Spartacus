using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneChoice : MonoBehaviour {

    [Header("子オブジェクトのImageのチェックボックスは最初は外しておく")]
    [SerializeField,Header("RetryButtonShadow(Image)を入れる")]
    private Image ButtonShadowLeft;
    [SerializeField,Header("TitleButtonShadow(Image)を入れる")]
    private Image ButtonShadowRight;
    [SerializeField]
    private GameOverAnim gameoveranim;

    private int SceneChange = 0;　//0か1でどのシーンに飛ぶか決める
    private bool Decision; //決定ボタンを押したときのフラグ

    private float Horizontal; //左右キーに使う
    [SerializeField,Header("選択音02を入れる")]
    private AudioClip OverSelectsound;
    [SerializeField,Header("決定音(ぽーん)を入れる")]
    private AudioClip OverDecisionsound;

    private AudioSource GameoveraudioSource;

    void Start ()
    {
        GameoveraudioSource = GetComponent<AudioSource>();
    }
	
	void Update ()
    {
        Horizontal = Input.GetAxis("Horizontal");

        //キー入力時の処理[]
        //if (Input.GetKeyDown(KeyCode.RightArrow) && SceneChange == 0)  
        //{
        //    audioSource.PlayOneShot(sound1);
        //    gameoveranim.SCENECHANGETIMER = 5;                          
        //    SceneChange = 1;    
        //}                                                               
        //if (Input.GetKeyDown(KeyCode.LeftArrow) && SceneChange == 1)
        //{
        //    audioSource.PlayOneShot(sound1);
        //    gameoveranim.SCENECHANGETIMER = 5;
        //    SceneChange = 0;
        //}
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    audioSource.PlayOneShot(sound2);
        //    gameoveranim.SCENECHANGETIMER = 5;
        //    Decision = true;
        //}

        //ゲームパットを使うときの移動

        if (Horizontal == 1 && SceneChange == 0)
        {
            GameoveraudioSource.PlayOneShot(OverSelectsound);
            gameoveranim.SCENECHANGETIMER = 5;        //GameOverAnimで使うTimeを５にすることでボタンを押したらアニメーションを終了する                         
            SceneChange = 1;
        }

        if (Horizontal == -1 && SceneChange == 1)
        {
            GameoveraudioSource.PlayOneShot(OverSelectsound);
            gameoveranim.SCENECHANGETIMER = 5;
            SceneChange = 0;
        }

        if (Input.GetButtonDown("Circle"))
        {
            GameoveraudioSource.PlayOneShot(OverDecisionsound);
            gameoveranim.SCENECHANGETIMER = 5;
            Decision = true;
        }

        //0か1かでどのボタンを押しているか影を出す[0でリトライボタン。1でタイトルへ戻るボタン]
        if (SceneChange == 0)
        {
            ButtonShadowLeft.GetComponent<Image>().enabled = true;
            ButtonShadowRight.GetComponent<Image>().enabled = false;
        }
        if(SceneChange == 1)
        {
            ButtonShadowRight.GetComponent<Image>().enabled = true;
            ButtonShadowLeft.GetComponent<Image>().enabled = false;
        }

        //どのボタンを押しているか上の処理を取得してシーンへ飛ばす
        if(SceneChange == 0 && Decision == true)
        {
            SceneManager.LoadScene("main");
        }
        else if(SceneChange == 1 && Decision == true)
        {
            SceneManager.LoadScene("Title");
        }
        //Debug.Log(SceneChange);
        // if(Input.GetButton("Circle"))//○ボタン
        // if(Horizontal == 1) 右ボタン
        // if(Horizontal == -1) 左ボタン
    }
}
