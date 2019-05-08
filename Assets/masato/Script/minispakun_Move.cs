using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minispakun_Move : MonoBehaviour {
    [SerializeField, Header("スパ君のスピード")]
    private float minisupaSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(minisupaSpeed, 0, 0);
    }
}
