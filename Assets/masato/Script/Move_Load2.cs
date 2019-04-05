using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Load2 : MonoBehaviour
{
    float LoadTime2;
    public Material[] _material2;           // 割り当てるマテリアル. 
    private int j;

    public float z2;

    // Use this for initialization
    void Start()
    {
        j = 0;
        LoadTime2 = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(z2, 0.0f, 0.0f);
        LoadTime2 += 0.1f;
        if (transform.position.x < -13.7f)
        {
            transform.position = new Vector3(16.63f, -2, -4.5f);
        }

        if (LoadTime2 > 160)
        {
            j++;
            if (j == 3)
            {
                j = 0;
            }

            this.GetComponent<MeshRenderer>().material = _material2[j];
        }
        Debug.Log(LoadTime2);//513でだいたい90秒
        Debug.Log(j);//513でだいたい90秒
    }
}

