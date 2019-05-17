using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendController_Main : MonoBehaviour {
    private EscapeEnemyController_Main escapeController;
    private Animator animator;
    private GameObject escapeEnemy;
    private float timer;

    public GameObject _EscapeEnemy { set { escapeEnemy = value; } }

    void Start()
    {
        escapeController = escapeEnemy.GetComponent<EscapeEnemyController_Main>();
        animator = GetComponent<Animator>();
    }

    void Update()
    // void FixidUpdate()
    {
        if (escapeController._STATE == EscapeEnemyController_Main.STATE._ESCAPE)
        {
            timer += Time.deltaTime;
            if (timer > 0.2f)
            {
                Debug.Log("ESCAPE");
                animator.SetBool("Chase", true);
                transform.position += transform.forward * Time.deltaTime;
            }
        }
    }
}
