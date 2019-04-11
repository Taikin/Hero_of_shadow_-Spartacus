using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Load : MonoBehaviour
{
    float LoadTime;
    public Material[] _material;           // 割り当てるマテリアル. 
    private int i;

    public float z1;

    // Use this for initialization
    void Start()
    {
        i = 0;
        LoadTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(z1, 0.0f, 0.0f);
        LoadTime += 0.1f;
        if (transform.position.x < -13.7f)
        {
            transform.position = new Vector3(16.93f, -2, -4.5f);
        }

        if (LoadTime > 100)
        {
            i++;
            if (i == 3)
            {
                i = 0;
            }

            this.GetComponent<MeshRenderer>().material = _material[i];
        }
        Debug.Log(LoadTime);//513でだいたい90秒
        Debug.Log(i);//513でだいたい90秒
    }
}

