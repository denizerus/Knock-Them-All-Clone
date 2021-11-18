using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrowBall : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform ballStartPoint;
    private Transform playerPoint;
    public float minShotTime, maxShotTime;
    Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        playerPoint = GameObject.Find("PlayerTargetPos").transform;
        minShotTime = 3f;
        maxShotTime = 10f;
        StartCoroutine(StartThrowing());
    }

    public IEnumerator StartThrowing()
    {
        yield return new WaitForSeconds(Random.Range(minShotTime, maxShotTime));
        anim.SetBool("isEnemyThrow", true);
    }

    public void ThrowEnemyBall()
    {
        anim.SetBool("isEnemyThrow", false);
        var enemyBall = Instantiate(ballPrefab, ballStartPoint);
        Rigidbody rb = enemyBall.GetComponent<Rigidbody>();
        rb.useGravity = true;
        Vector3 enemyBallVector = playerPoint.position - transform.position;
        float randomY = Random.Range(enemyBallVector.y - 0.2f, enemyBallVector.y + 0.2f);
        rb.AddForce(playerPoint.position - transform.position * 30f);
        StartCoroutine(StartThrowing());
    }
}
