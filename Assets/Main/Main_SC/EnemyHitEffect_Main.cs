using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitEffect_Main : MonoBehaviour {

    GameObject Effect;
    private float DeleteTime = 0.2f;
    float time;
    private bool EffectFlg;

	// Use this for initialization
	void Start () {
        Effect = transform.GetChild(0).gameObject;
        EffectFlg = false;
        time = 0;
	}

    private void OnTriggerEnter(Collider Arrow)
    {
        if (Arrow.gameObject.tag == "enemy" && EffectFlg == false)
        {
            EffectFlg = true;
        }
    }

    // Update is called once per frame
    void Update () {
       Debug.Log(DeleteTime);
        if (EffectFlg == true)
        {
            time += Time.deltaTime;
            Effect.SetActive(true);
        }

        if (DeleteTime < time)
        {
            EffectFlg = false;
            Effect.SetActive(false);
            time = 0;
        }
    }
}
