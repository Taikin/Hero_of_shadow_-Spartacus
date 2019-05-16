using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendController : MonoBehaviour
{
    private EscapeEnemyController escapeController;
    private Animator animator;
    private GameObject escapeEnemy;

    public GameObject _EscapeEnemy { set { escapeEnemy = value; } }

	void Start ()
    {
        escapeController =escapeEnemy.GetComponent<EscapeEnemyController>();
        animator = GetComponent<Animator>();
	}
	
	void Update ()
    {
        if (escapeController._STATE == EscapeEnemyController.STATE._ESCAPE)
        {
            Debug.Log("ESCAPE");
            animator.SetBool("Chase", true);
            transform.position += transform.forward * Time.deltaTime;
        }
    }
}
