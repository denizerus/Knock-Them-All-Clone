using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitsPlayer : MonoBehaviour
{
    Animator anim;
    public bool gameContinue;

    private void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        gameContinue = true;
    }

    private void Update()
    {
        if (!gameContinue)
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (GameObject ball in balls)
            {
                if (ball.GetComponent<Ball>().isUsable == false)
                {
                    Destroy(ball);
                }
                else if (ball.GetComponent<Ball>().isUsable)
                {
                    ball.GetComponent<DragAndShoot>().enabled = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "EnemyBall")
        {
            if (anim.GetBool("isThrow"))
            {
                //ENEMY HITS PLAYER
                GameManager.Instance.StopAllCoroutines();
                LevelManager myLevelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
                myLevelManager.RetryTheLevel();
                gameContinue = false;
            }
        }
    }
}
