using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityArrowController_Main : MonoBehaviour
{
    [SerializeField, Header("矢の影")]
    private GameObject arrowImage;

    private ArrowController_Main arrowController;
    private Vector3 charaPos;
    private Rigidbody rb;
    private bool hit;
    private float rotatespeed;
    private float rotatespeedCurve;
    public bool _Hit { get { return hit; } set { hit = value; } }
    public Vector3 _CharaPos { set { charaPos = value; } }

    void Start()
    {
        arrowController = arrowImage.GetComponent<ArrowController_Main>();
        // 回転量を格納
        rotatespeed = arrowController._RotateSpeed;
        rotatespeedCurve = arrowController._RotateSpeedCurve;
        rb = GetComponent<Rigidbody>();
    }

    void Update ()
        // void FixidUpdate
    {
        // 真ん中以外に当たったら
        if (arrowController._Protect)
        {
            //rb.useGravity = true;
            if (arrowController._ArrowState != ArrowController_Main.ArrowState._CURVE_LINE) {
                rb.velocity = new Vector3(0, -0.67f, 0);
                transform.Rotate(0, -rotatespeed, 0);    //オブジェクトを回す
            }
            else if (arrowController._ArrowState == ArrowController_Main.ArrowState._CURVE_LINE)
            {
                rb.velocity = new Vector3(0, -0.73f, 0);
                transform.Rotate(0, -rotatespeedCurve, 0);    //オブジェクトを回す

            }
        }

        if (transform.position.y < 0.68)
        {
            Destroy(arrowImage);
            Destroy(this.gameObject);
        }
    }

    public void DestroyArrow()
    {
  
            Destroy(arrowImage);
     }
}
