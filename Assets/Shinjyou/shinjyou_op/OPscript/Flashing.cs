using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Flashing : MonoBehaviour {
    [SerializeField]
    private Image SweatImage;
    int a;
    private GameObject opgameobject;
    [SerializeField]
    private OpnigAnim opniganim;


    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (opniganim.SweatImg_Flg == false)
        {
            a += 1;
            if (a > 30)
            {
                SweatImage.GetComponent<Image>().enabled = false;
            }
            if (a > 60)
            {
                SweatImage.GetComponent<Image>().enabled = true;
                a = 0;
            }
        }
        if(opniganim.SweatImg_Flg)
        {
            SweatImage.GetComponent<Image>().enabled = false;
        }
        //Debug.Log(a);
	}
}
