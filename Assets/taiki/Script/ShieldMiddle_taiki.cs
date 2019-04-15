using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMiddle_taiki : MonoBehaviour {
    // Use this for initialization
    GameObject Effectobj;
    bool middleEffect;      
    float Rimittime=0.5f;   //エフェクトが消える時間
    float time;
    AudioSource audiosource;
    public AudioClip Middleshieldsound;   //真ん中に当たった時の盾の音
    void Start () {
        Effectobj = transform.GetChild(0).gameObject;
        audiosource = GetComponent<AudioSource>();

    }
    //真ん中に当たったらエフェクトを出す
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Effectobj.SetActive(true);
        middleEffect = true;
        audiosource.PlayOneShot(Middleshieldsound);   //真ん中に当たった時の音再生
    }

  
    // Update is called once per frame
    void Update () {
        if (middleEffect==true)
        {
            time += Time.deltaTime;
            //時間を過ぎるとエフェクトを出さない
            if (Rimittime < time)
            {
                time = 0;
                Effectobj.SetActive(false);
            }
        }
    }
}
