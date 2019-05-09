using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ClearSceneChange : MonoBehaviour {
    private float SceneChamgeTimer;
    private bool SceneChamge_Flg;
    private AudioSource ClearaudioSource;
    [SerializeField,Header("決定音(ポーン)を入れる")]
    private AudioClip ClearSelectOn;


    void Start ()
    {
        ClearaudioSource = GetComponent<AudioSource>();
    }
	

	void Update ()
    {
        
        //if (Input.GetKeyDown(KeyCode.Space)) //if(Input.GetButton("Circle"))  //たぶん○ボタン押したらできる
        //{
        //    SceneChamge_Flg = true;
        //}
        //if (SceneChamge_Flg == true)
        //{
        //    SceneChamgeTimer += Time.deltaTime;

        //    if (SceneChamgeTimer >= 1.0f)
        //    {
        //        SceneManager.LoadScene("Title");
        //    }
        //}
        //Debug.Log(SceneChamgeTimer);
        if (Input.GetButtonDown("Circle"))  //たぶん○ボタン押したらできる
        {
            ClearaudioSource.PlayOneShot(ClearSelectOn);
            SceneChamge_Flg = true;
        }
        if (SceneChamge_Flg == true)
        {
            SceneChamgeTimer += Time.deltaTime;

            if (SceneChamgeTimer >= 1.0f)
            {
                SceneManager.LoadScene("Title");
            }
        }
    }
}
