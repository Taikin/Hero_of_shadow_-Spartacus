using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniSpakun_Main : MonoBehaviour {
    [SerializeField, Header("スパ君のスピード")]
    private float minisupaSpeed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(minisupaSpeed, 0, 0);
    }
}
