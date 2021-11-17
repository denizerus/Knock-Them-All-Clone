using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "EnemyBall")
        {
            Destroy(collision.gameObject);
        }
    }
}
