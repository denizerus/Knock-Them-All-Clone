using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitsPlayer : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyBall")
        {
            if (anim.GetBool("isThrow"))
            {
                //ENEMY CAN HIT PLAYER
            }
            else
            {
                //ENEMY CAN NOT HIT PLAYER
            }
        }
    }
}
