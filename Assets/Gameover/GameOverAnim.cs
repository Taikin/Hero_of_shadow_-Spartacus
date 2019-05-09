using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverAnim : MonoBehaviour {
    [SerializeField,Header("GameOverImg(Image)を入れる")]
    private Image gameoverimg;
    [SerializeField,Header("GameOverImg(Image)の値を変えるようにする")]
    private float pos_y;
    //GameOverImg(Image)の動く速さ
    private float increment = 0.3f;
    private float SceneChangeTimer;
    public float SCENECHANGETIMER
    {
        get { return SceneChangeTimer; }
        set { SceneChangeTimer = value; }
    }
    
    private int moving_flg;         //上下に動くときにバウンドして見せるよう間隔を調整するフラグ
    private float SlopeImage = 2;   //GameOverImageを傾ける数字

    private enum MOVEIMAGE_TYPE
    {
        UP,
        DOWN,
        STOP,
    }

    private MOVEIMAGE_TYPE type = MOVEIMAGE_TYPE.DOWN;

    void Start()
    {

    }

    void Update()
    {
        SceneChangeTimer += Time.deltaTime;

        switch (type)
        {
            case MOVEIMAGE_TYPE.UP:
                pos_y += increment;
                gameoverimg.GetComponent<RectTransform>().position = new Vector3(0, pos_y, 0);
                break;
            case MOVEIMAGE_TYPE.DOWN:
                pos_y -= increment;
                gameoverimg.GetComponent<RectTransform>().position = new Vector3(0, pos_y, 0);
                break;
            case MOVEIMAGE_TYPE.STOP:
                pos_y = 5;
                gameoverimg.GetComponent<RectTransform>().position = new Vector3(0, pos_y, 0);
                break;
        }

        if (SceneChangeTimer <= 2.5f)
        {
            if (pos_y <= 5)
            {
                moving_flg += 1;
            }

            if (moving_flg <= 3)
            {
                MoveUP();
                MoveDown();
            }
            if (moving_flg == 4)
            {
                SlopeImage = 0;
                GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, SlopeImage);
                type = MOVEIMAGE_TYPE.STOP;
            }
        }
        else
        {
            SlopeImage = 0;
            GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, SlopeImage);
            type = MOVEIMAGE_TYPE.STOP;
        }
    }
    void MoveUP()
    {
        if (pos_y <= 5)
        {
            GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, -SlopeImage);
            type = MOVEIMAGE_TYPE.UP;
            if(moving_flg == 1)
            {
                increment = 0.2f;
            }
            if(moving_flg == 2)
            {
                SlopeImage = 0.5f;
                increment = 0.15f;
            }
            if (moving_flg == 3)
            {
                SlopeImage = 0.5f;
                increment = 0.1f;
            }
        }
    }
    void MoveDown()
    {
        
        if (pos_y >= 20)
        {
            type = MOVEIMAGE_TYPE.DOWN;
        }
        if (pos_y >= 12 && moving_flg == 1)
        {
            type = MOVEIMAGE_TYPE.DOWN;
        }
        if (pos_y >= 8 && moving_flg == 2)
        {
            type = MOVEIMAGE_TYPE.DOWN;
        }
        if (pos_y >= 6 && moving_flg == 3)
        {
            GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, SlopeImage);
            type = MOVEIMAGE_TYPE.DOWN;
        }
    }
}