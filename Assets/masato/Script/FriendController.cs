using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendController : MonoBehaviour
{
    private EscapeEnemyController escapeController;
    private Animator animator;
    private GameObject escapeEnemy;
    private bool stopFlg;

    public GameObject _EscapeEnemy { set { escapeEnemy = value; } }
    public bool _StopFlg { set { stopFlg = value; } }

	void Start ()
    {
        escapeController =escapeEnemy.GetComponent<EscapeEnemyController>();
        animator = GetComponent<Animator>();
	}
	
	void Update ()
    {
        if (escapeController._STATE == EscapeEnemyController.STATE._ESCAPE && !stopFlg)
        {
            Debug.Log("ESCAPE");
            animator.SetBool("Chase", true);
            transform.position += transform.forward * Time.deltaTime;
        }
    }
}
