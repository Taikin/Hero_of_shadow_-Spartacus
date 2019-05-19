using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowShieldOther_Main : MonoBehaviour {

    GameObject shieldEffectobj;
    bool shieldEffect;
    float Rimittime = 0.2f;   //エフェクトが消える時間
    float time;
    AudioSource audiosource;
    public AudioClip shieldsound;   //真ん中以外に当たった時の盾の音


    public bool _ShieldEffect { get { return shieldEffect; } }

    // Use this for initialization
    void Start()
    {
        //子を取得
       shieldEffectobj = transform.GetChild(0).gameObject;
        audiosource = GetComponent<AudioSource>();
    }


    //真ん中以外の盾に当てた時のエフェクト出す
    private void OnTriggerEnter2D(Collider2D shieldpoint)
    {
        if(shieldpoint.gameObject.tag == "Arrow") {
            var ArrowController = shieldpoint.gameObject.GetComponent<ArrowController_Main>();
            if (ArrowController._Protect/* && !ArrowController._Middle*/)
            {
                shieldEffectobj.transform.position = (Vector2)shieldpoint.transform.position;
                shieldEffectobj.SetActive(true);
                shieldEffect = true;
                Vector3 pos = shieldEffectobj.transform.position;
                pos.z = 90;  //出現するz軸を調整
                shieldEffectobj.transform.position = pos;
                audiosource.PlayOneShot(shieldsound);   //真ん中以外に当たった時の音再生
            }
        }

    }

    // Update is called once per frame
    //void Update()
    void FixedUpdate()
    {
        if (shieldEffect == true)
        {
            time += Time.deltaTime;
            //時間を過ぎるとエフェクトを出さない
            if (Rimittime < time)
            {
                shieldEffect = false;
                time = 0;
                shieldEffectobj.SetActive(false);
            }
        }
    }
}
