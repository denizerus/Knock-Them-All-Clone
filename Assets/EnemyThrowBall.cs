using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrowBall : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform ballStartPoint;
    public float minShotTime, maxShotTime;
    private Transform playerPoint;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        playerPoint = GameObject.Find("PlayerTargetPos").transform;
        minShotTime = 3f;
        maxShotTime = 5f;
        StartCoroutine(StartThrowing());
    }

    // Update is called once per frame
    void Update()
    {

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
        rb.AddForce(playerPoint.position - transform.position  * 30f);

        StartCoroutine(StartThrowing());
    }
}
