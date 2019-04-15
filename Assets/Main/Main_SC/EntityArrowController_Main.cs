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
    public bool _Hit { get { return hit; } set { hit = value; } }
    public Vector3 _CharaPos { set { charaPos = value; } }

    void Start()
    {
        arrowController = arrowImage.GetComponent<ArrowController_Main>();
        // 回転量を格納
        rotatespeed = arrowController._RotateSpeed;
        rb = GetComponent<Rigidbody>();
    }
    
	void Update ()
    {
        // 真ん中以外に当たったら
		if(arrowController._Protect)
        {
            //rb.useGravity = true;
            rb.velocity = new Vector3(0, -0.2f, 0);
            transform.Rotate(0, 0, rotatespeed);    //オブジェクトを回す
        }
	}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "ground")
    //    {
    //        Destroy(arrowImage);
    //        Destroy(this.gameObject);
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "ground")
    //    {
    //        Destroy(arrowImage);
    //        Destroy(this.gameObject);
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ground")
        {
            Destroy(arrowImage);
            Destroy(this.gameObject);
        }
    }

    public void DestroyArrow()
    {
        Destroy(arrowImage);
        Destroy(this.gameObject);
    }
}
