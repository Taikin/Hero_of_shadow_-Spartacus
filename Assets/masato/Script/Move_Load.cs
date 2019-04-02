using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Load : MonoBehaviour
{
    float LoadTime;
    // Use this for initialization
    void Start()
    {
        LoadTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-0.1f, 0.0f, 0.0f);
        LoadTime += 1.0f;
        if (transform.position.x < -14.0f)
        {
            transform.position = new Vector3(16.7f, -2, -4.5f);
        }
        if (LoadTime > 100)
        {
            // 赤色に変更する
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
        if (LoadTime > 300)
        {
            // 黄緑色に変更する
            gameObject.GetComponent<Renderer>().material.color = new Color(173, 255, 47);
        }
        Debug.Log(LoadTime);
    }
}
