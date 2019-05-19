using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowShieldMidlle_Main : MonoBehaviour {


    GameObject Effectobj;
    bool middleEffect;
    float Rimittime = 0.2f;   //エフェクトが消える時間
    float time;
    AudioSource audiosource;
    public AudioClip Middleshieldsound;   //真ん中に当たった時の盾の音

    public bool _middleEffect { get { return middleEffect; } }

    void Start()
    {
        Effectobj = transform.GetChild(0).gameObject;
        audiosource = GetComponent<AudioSource>();
    }
    //真ん中に当たったらエフェクトを出す
    private void OnTriggerEnter2D(Collider2D middlepoint)
    {
        if (middlepoint.gameObject.tag == "Arrow" )
        {
            var ArrowController = middlepoint.gameObject.GetComponent<ArrowController_Main>();
            if (ArrowController._Middle)
            {
                Effectobj.SetActive(true);
                middleEffect = true;
                audiosource.PlayOneShot(Middleshieldsound);   //真ん中に当たった時の音再生
            }
        }

    }
    // Update is called once per frame
    //void Update()
    void FixedUpdate()
    {
        if (middleEffect == true)
        {
            time += Time.deltaTime;
            //時間を過ぎるとエフェクトを出さない
            if (Rimittime < time)
            {
                middleEffect = false;
                time = 0;
                Effectobj.SetActive(false);
            }
        }
    }
}
