﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemyPoint : MonoBehaviour
{
    Rigidbody rig;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().numOfEnemy--;
            EnemyThrowBall etb = gameObject.transform.parent.gameObject.transform.Find("EnemyModel").GetComponent<EnemyThrowBall>();
            etb.StopAllCoroutines();
            etb.enabled = false;
            StartCoroutine(FadeOutAndDestroy(1f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            rig = gameObject.transform.parent.gameObject.transform.Find("EnemyModel").GetComponent<Rigidbody>();
            rig.AddForce(new Vector3(0, 500f, 0));
            StartCoroutine(FadeOutAndDestroy(1f));
        }
    }

    IEnumerator FadeOutAndDestroy(float time)
    {
        yield return new WaitForSeconds(2f); ;
        Destroy(gameObject.transform.parent.gameObject);
    }
}
