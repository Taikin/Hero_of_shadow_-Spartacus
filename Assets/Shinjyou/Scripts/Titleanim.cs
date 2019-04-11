using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Titleanim : MonoBehaviour {
    private RectTransform recttransform;
    private float width = 400;      //タイトルの最初の画像の大きさ(幅)
    private float height = 400;     //タイトルの最初の画像の大きさ(高さ)
    [SerializeField,Header("TitleImgをアタッチする")]
    private Image titleimage;       //[TitleImg]を入れる
    //private float fadetimer;
    private float Timagealfa;       //タイトル画像の透明度

    // Use this for initialization
    void Start ()
    {
        recttransform = GameObject.Find("TitleImg").GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        width -= 20f;
        height -= 20f;
        if (width >= 100)
        {
            recttransform.sizeDelta = new Vector2(width, height);
            //fadetimer += Time.deltaTime;
            Timagealfa += 0.1f;
            titleimage.GetComponent<Image>().color = new Color(1, 1, 1, Timagealfa);
        }

    }
}