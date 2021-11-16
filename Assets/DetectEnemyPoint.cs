using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemyPoint : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("GOOOOOOOOOOLLLL");
            gameObject.GetComponent<BoxCollider>().enabled = false;
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().numOfEnemy--;
        }
    }
}
