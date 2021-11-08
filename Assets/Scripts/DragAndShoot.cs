using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShoot : MonoBehaviour
{

    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;

    private Rigidbody rb;

    private bool isShoot;

    private float forceMultiplier = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //     Debug.Log("start");
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
                //     Debug.Log("onmouseup" + Input.mousePosition);
                mouseReleasePos = Input.mousePosition;
                Shoot(mousePressDownPos - mouseReleasePos);
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Debug.Log("calisti");

                Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
                Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.y)) * forceMultiplier;

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
        //     Debug.Log("onmouseup" + Input.mousePosition);
        mouseReleasePos = Input.mousePosition;
        Shoot(mousePressDownPos - mouseReleasePos);
    }


    private void OnMouseDrag()
    {
        Debug.Log("onmousedrag calisti");

        Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
        Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.y)) * forceMultiplier;

        if (!isShoot)
        {
            DrawTrajectory.Instance.UpdateTrajectory(forceV, rb, transform.position);
        }
    }




    void Shoot(Vector3 Force)
    {
        //    Debug.Log("shoot 1");
        if (isShoot)
            return;
        //  Debug.Log("shoot 2");
        rb.useGravity = true;
        rb.AddForce(new Vector3(Force.x, Force.y, Force.y) * forceMultiplier);
        isShoot = true;
        //Debug.Log("shoot 3");
    }
}
