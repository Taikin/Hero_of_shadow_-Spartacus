using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeOut : MonoBehaviour {
    [SerializeField]
    private Image Clearfade;
    private float clearalfa = 0;
    private bool ButtonOn_Flg;

	void Start () {
		
	}


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //if(Input.GetButtonDown("Circle"))  //たぶん○ボタン押したらできる
        {
            ButtonOn_Flg = true;
        }
        if (ButtonOn_Flg == true)
        {
            clearalfa += 0.2f;
            Clearfade.GetComponent<Image>().color = new Color(0, 0, 0, clearalfa);
        }
    }
}
