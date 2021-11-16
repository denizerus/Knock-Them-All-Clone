using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool isUsable;

    public float expForce, radius;

    private void Start()
    {
        expForce = 100f;
        radius = 2f;
        isUsable = true;
    }


    private void OnCollisionEnter(Collision other)
    {
        KnockBack();
    }

    void KnockBack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearby in colliders)
        {
            Rigidbody rig = nearby.GetComponent<Rigidbody>();
            if (rig != null &&  nearby.transform.tag != "Ball" )
            {
                rig.AddExplosionForce(expForce, transform.position, radius);
            }
        }
    }
}
