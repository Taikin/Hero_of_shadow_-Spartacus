using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFlg_Main : MonoBehaviour {

    GameObject Effect;
    private float DeleteTime = 0.2f;
    float time;
    private bool EffectFlg;


    // Use this for initialization
    void Start () {
        Effect = transform.GetChild(2).gameObject;
        EffectFlg = false;
        time = 0;

    }

    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.gameObject.tag == "Arrow" && EffectFlg == false)
        {
            EffectFlg = true;
            time += Time.deltaTime;
            Effect.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update () {
        if (DeleteTime < time)
        {
            EffectFlg = false;
            Effect.SetActive(false);
            time = 0;
        }

    }
}
