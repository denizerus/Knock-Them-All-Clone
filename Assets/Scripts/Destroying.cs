using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroying : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
