using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shieldshield_taiki : MonoBehaviour {
    GameObject shieldEffectobj;
    bool shieldEffect;
    float Rimittime = 0.5f;   //エフェクトが消える時間
    float time;
    AudioSource audiosource;
    public AudioClip shieldsound;   //真ん中以外に当たった時の盾の音

    // Use this for initialization
    void Start () {
        //子を取得
        shieldEffectobj = transform.GetChild(0).gameObject;
        audiosource = GetComponent<AudioSource>();
    }


    //真ん中以外の盾に当てた時のエフェクト出す
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //当たった位置に出す
        foreach (ContactPoint2D point in collision.contacts)
        {

            Debug.Log(point.point);
            shieldEffectobj.transform.position = (Vector2)point.point;
            shieldEffectobj.SetActive(true);
            shieldEffect = true;
        }
        Vector3 pos = shieldEffectobj.transform.position;
        pos.z = 90;  //出現するz軸を調整
        shieldEffectobj.transform.position = pos;
        audiosource.PlayOneShot(shieldsound);   //真ん中以外に当たった時の音再生
    }

    // Update is called once per frame
    void Update () {
        if (shieldEffect == true)
        {
            time += Time.deltaTime;
            //時間を過ぎるとエフェクトを出さない
            if (Rimittime < time)
            {
                time = 0;
                shieldEffectobj.SetActive(false);
            }
        }
    }
}
