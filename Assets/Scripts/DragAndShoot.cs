using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShoot : MonoBehaviour
{
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;

    private Rigidbody rb;
    
    private float force = 2f;

    private bool isShoot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            mousePressDownPos = Input.mousePosition;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            DrawTrajectory.Instance.HideLine();
            DoMouseUpFuntions();
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {

            Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
            Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.y)) * force;

            Debug.Log("forceV: " + forceV);

            if (!isShoot)
            {
                DrawTrajectory.Instance.UpdateTrajectory(forceV, rb, transform.position);
            }
        }
    }

    private void OnMouseDown()
    {
        mousePressDownPos = Input.mousePosition;
    }

    private void OnMouseUp()
    {
        DrawTrajectory.Instance.HideLine();
        DoMouseUpFuntions();
    }

    void DoMouseUpFuntions()
    {
        Vector3 vectorF;
        vectorF = new Vector3();
        mouseReleasePos = Input.mousePosition;
        if (mousePressDownPos.y - mouseReleasePos.y > 0)
            vectorF = mousePressDownPos - mouseReleasePos;
        else if (mousePressDownPos.y - mouseReleasePos.y <= 0)
            vectorF = mouseReleasePos- mousePressDownPos;

        Shoot(vectorF);
    }


    private void OnMouseDrag()
    {
        Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
        Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.y)) * force;

        //Debug.Log("forceV: " + forceV);

        if (!isShoot)
        {
            DrawTrajectory.Instance.UpdateTrajectory(forceV, rb, transform.position);
        }
    }


    public void Shoot(Vector3 Force)
    {
        if (isShoot)
            return;
        rb.useGravity = true;
        rb.AddForce(new Vector3(Force.x, Force.y, Force.y) * force);
        isShoot = true;
        GameManager.Instance.NewSpawnRequest();
    }
}
