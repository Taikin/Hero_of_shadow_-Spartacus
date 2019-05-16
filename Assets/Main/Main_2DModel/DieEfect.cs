using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEfect : MonoBehaviour {

    //[SerializeField]
    //private GameObject RightStar;
    //[SerializeField]
    //private GameObject LeftStar;
    [SerializeField]
    private GameObject hitefect;
    //private float a;
    //private float b = 1;
    private float colorbrend;
	// Use this for initialization
	void Start ()
    {
		 
	}
	
	// Update is called once per frame
	void Update ()
    {
        //RightStar.transform.position += new Vector3(0.08f, 0.15f, 0);
        //RightStar.transform.localScale += new Vector3(0.05f, 0.05f,0.05f);
        //LeftStar.transform.position += new Vector3(-0.08f, 0.15f, 0);
        //LeftStar.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);

        //a += 0.1f;
        //if(a >= 2)
        //{
        //    b -= 0.08f;
        //    //Debug.Log(a);
        //    RightStar.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, b);
        //    LeftStar.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, b);
        //}
        //if(a >= 5.5)
        //{
        //    this.gameObject.SetActive(false);
        //}
        colorbrend += 0.2f;
        hitefect.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, colorbrend);
        if(colorbrend <= 1)
        {
            hitefect.transform.localScale += new Vector3(0.1f,0.1f,0.1f); 
        }
        if(colorbrend >= 5)
        {
            this.gameObject.SetActive(false);
        }
    }
}
