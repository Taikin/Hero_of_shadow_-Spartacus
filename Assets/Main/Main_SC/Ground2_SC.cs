using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground2_SC : MonoBehaviour {

    float LoadTime2;
    public Material[] _material2;           // 割り当てるマテリアル. 

    private float Position_x, Position_y, Position_z;

    private int Material_Num2;

    private float x2;
    public int ChangeNumber;


    // Use this for initialization
    void Start () {
        Material_Num2 = 0;
        x2 = -0.01f;
        ChangeNumber = 0;

        // ground2の初期位置
        Position_x = 1.7f;
        Position_y = 0.76f;
        Position_z = -9.5f;
        transform.position = new Vector3(Position_x, Position_y, Position_z);

    }

    // Update is called once per frame
    void Update () {
        transform.position += new Vector3(x2, 0.0f, 0.0f);
        if (transform.position.x < -2.0f)
        {
            Position_x = 2.0f;
            ChangeNumber += 1;
            transform.position = new Vector3(Position_x, Position_y, Position_z);
        }

        switch (ChangeNumber)
        {
            case 0:
                Material_Num2 = 0;
                break;

            case 4:
                Material_Num2 = 1;
                break;

            case 6:
                Material_Num2 = 2;
                break;
        }

        this.GetComponent<MeshRenderer>().material = _material2[Material_Num2];
        // Debug.Log(LoadTime2);//513でだいたい90秒
        // Debug.Log(j);//513でだいたい90秒

    }
}
