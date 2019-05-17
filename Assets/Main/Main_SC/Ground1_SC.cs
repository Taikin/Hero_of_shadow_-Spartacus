using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground1_SC : MonoBehaviour {

    float LoadTime;
    public Material[] _material;           // 割り当てるマテリアル. 
    private int Material_Num;

    private float Position_x, Position_y, Position_z;

    private float x1;
    public int ChangeNumber;


    // Use this for initialization
    void Start () {
        // ground_1の初期位置
        Position_x = -0.3f;
        Position_z = -9.5f;

        Material_Num = 0;
        ChangeNumber = 0;
        x1 = -0.01f;
        transform.position = new Vector3(Position_x, transform.position.y, Position_z);

    }

    // Update is called once per frame
    void Update ()
    // void FixidUpdate()
    {
        transform.position += new Vector3(x1, 0, 0);
        LoadTime += 0.1f;
        if (transform.position.x < -2.0f)
        {
            Position_x = 1.99f;
            ChangeNumber += 1;
            transform.position = new Vector3(Position_x, transform.position.y, Position_z);
        }

        switch (ChangeNumber)
        {
            case 0:
                Material_Num = 0;
                break;

            case 4:
                Material_Num = 1;
                break;

            case 6:
                Material_Num = 2;
                break;
        }

        this.GetComponent<MeshRenderer>().material = _material[Material_Num];
        // Debug.Log(LoadTime);//513でだいたい90秒
        // Debug.Log(i);//513でだいたい90秒7
        //Debug.Log(ChangeNumber);
    }

    public int GetChangeNumber()
    {
        return ChangeNumber;
    }

}
