using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Load2 : MonoBehaviour
{
    float LoadTime2;
    public Material[] _material2;           // 割り当てるマテリアル. 

    public float z2;

    public int flg2;

    // Use this for initialization
    void Start()
    {
        LoadTime2 = 0.0f;
        //flg2 = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(z2, 0.0f, 0.0f);
        LoadTime2 += Time.deltaTime;
        if (transform.position.x < -2.17f)
        {
            transform.position = new Vector3(3.2f, 0.76f, -9.5f);
        }

        if (LoadTime2 > 40)
        {
            flg2++;
            LoadTime2 = 0;
        }
        if (flg2 > 2)
        {
            flg2 = 2;
        }
        this.GetComponent<MeshRenderer>().material = _material2[flg2];//flg２に入っているマテリアルを出す
    }
}

