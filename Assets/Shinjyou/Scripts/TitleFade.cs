using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleFade : MonoBehaviour {
    [SerializeField,Header("Fade-Inをアタッチする")]
    private Image fadein;       //ゲームの始まりのフェードイン画像
    //private float changetimer;  //
    private float alfa = 1;     //フェードインする時の画像の透明度
    void Start () {
		
	}
	
	void Update () {
        //changetimer += Time.deltaTime;
        alfa -= 0.3f;
        fadein.GetComponent<Image>().color = new Color(0, 0, 0, alfa);

    }
}
