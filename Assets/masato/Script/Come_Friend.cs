using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Come_Friend : MonoBehaviour {

    public GameObject Frend;
    float Times;
    bool Comeflg;

	// Use this for initialization
	void Start () {
        Comeflg = false;//仲間はいません
	}
	
	// Update is called once per frame
	void Update () {

        Times += Time.deltaTime;//時間を確認
        //Times += 0.1f;

        if (Times > 30){
            if (Comeflg == false)
            {
                Instantiate(Frend, new Vector3(1.7f, 0.895f, -9.608f), Frend.transform.rotation);//仲間を出します
            }
            Comeflg = true;
        }

        //Debug.Log(Times);
	}
}
