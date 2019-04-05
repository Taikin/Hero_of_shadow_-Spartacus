using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour {

    public float Width, Length;
    public float x, y;

	// Use this for initialization
	void Start () {
        Width = 1;
        Length = 1;
        x = 2.5f;
        y = 0;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localScale = new Vector2(Width, Length);
        this.transform.localPosition = new Vector2(x, y);

    }
}
