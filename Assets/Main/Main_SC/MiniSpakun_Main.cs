using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSpakun_Main : MonoBehaviour {
    [SerializeField, Header("スパ君のスピード")]
    private float minisupaSpeed;
    [SerializeField, Header("カメラ")]
    private GameObject myCamera;

    private ComeOn_Main comeOnMain;

    // Use this for initialization
    void Start()
    {
        comeOnMain = myCamera.GetComponent<ComeOn_Main>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!comeOnMain.stopFlg)
        {
            transform.position += new Vector3(minisupaSpeed, 0, 0);
        }
    }
}
