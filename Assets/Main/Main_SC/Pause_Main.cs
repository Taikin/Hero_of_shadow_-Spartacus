using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Main : MonoBehaviour {

    [SerializeField]
    //　ポーズした時に表示するUIのプレハブ
    // private GameObject pauseUIPrefab;
    private GameObject pauseUI;

    bool pauseFlg;

    //　ポーズUIのインスタンス
    //private GameObject pauseUIInstance;

    void Start()
    {
        pauseFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Option") && pauseFlg == false)
        {
            pauseFlg = true;

            if (/*pauseUIInstance == null*/ pauseFlg == true)
            {
                //pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
                pauseUI.SetActive(true);
                Time.timeScale = 0f;
            }
            //else
            //{
            //    //Destroy(pauseUIInstance);
            //    pauseUI.SetActive(false);
            //    Time.timeScale = 1f;
            //}
        } else if (Input.GetButtonDown("Option") && pauseFlg == true)
        {
            pauseFlg = false;
            pauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
