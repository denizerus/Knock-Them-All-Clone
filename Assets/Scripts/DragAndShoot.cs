using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragAndShoot : MonoBehaviour
{
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;


    public Transform throwPos;
    public Transform coverPos;


    private Rigidbody rb;

    private float force = 2f;

    private bool isShoot;

    public Text testText;

    public Text testText2;

    public Text testText3;

    public Text testText4;

    public Text testText5;

    public Text testText6;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        coverPos = GameObject.Find("GameCharCoverPos").transform;
        throwPos = GameObject.Find("GameCharThrowPos").transform;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        testText = GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
        testText2 = GameObject.FindGameObjectWithTag("Text2").GetComponent<Text>();
        testText3 = GameObject.FindGameObjectWithTag("Text3").GetComponent<Text>();
        testText4 = GameObject.FindGameObjectWithTag("Text4").GetComponent<Text>();
        testText5 = GameObject.FindGameObjectWithTag("Text5").GetComponent<Text>();
        testText6 = GameObject.FindGameObjectWithTag("Text6").GetComponent<Text>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Handle native touch events
        foreach (Touch touch in Input.touches)
        {
            HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
        }

        // Simulate touch events from mouse events
        if (Input.touchCount == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
            }
            if (Input.GetMouseButton(0))
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
            }
            if (Input.GetMouseButtonUp(0))
            {
                HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
            }
        }
    }

    private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase)
    {
        switch (touchPhase)
        {
            case TouchPhase.Began:
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                testText.text = "Nfire 1";
            //    Debug.Log("PHASE 1");
                mousePressDownPos = Input.mousePosition;
                anim.SetBool("isThrow", true);
                anim.SetBool("isStandToCover", false);
                StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<MoveToCorrectPlace>().MoveChar(throwPos));
                break;
            case TouchPhase.Moved:
                testText2.text = "NtouchC2";
             //   Debug.Log("PHASE 2");
                Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
                Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.y)) * force;

            //    Debug.Log("forceV: " + forceV);

                if (!isShoot)
                {
                    DrawTrajectory.Instance.UpdateTrajectory(forceV, rb, transform.position);
                }
                break;
            case TouchPhase.Ended:
                testText3.text = "fire3";
         //       Debug.Log("PHASE 3");
                anim.SetBool("isThrow", false);
                anim.SetBool("isStandToCover", true);
                StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<MoveToCorrectPlace>().MoveChar(coverPos));
                DrawTrajectory.Instance.HideLine();
                DoMouseUpFuntions();
                break;
        }
    }



    /*
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            testText.text = "fire 1";
            Debug.Log("PHASE 1");
            mousePressDownPos = Input.mousePosition;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            testText2.text = "touchC2";
            Debug.Log("PHASE 2");
            Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
            Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.y)) * force;

            Debug.Log("forceV: " + forceV);

            if (!isShoot)
            {
                DrawTrajectory.Instance.UpdateTrajectory(forceV, rb, transform.position);
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            testText3.text = "fire3";
            Debug.Log("PHASE 3");
            DrawTrajectory.Instance.HideLine();
            DoMouseUpFuntions();
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("MOUSE PHASE 1");
        testText4.text = "mDown";
        mousePressDownPos = Input.mousePosition;
        anim.SetBool("isStandToCover", false);
        
        StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<MoveToCorrectPlace>().MoveChar(throwPos));
    }

    private void OnMouseDrag()
    {
        anim.SetBool("isThrow", true);
        Debug.Log("MOUSE PHASE 2");
        testText5.text = "mDrag";
        Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
        Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.y)) * force;

        //Debug.Log("forceV: " + forceV);

        if (!isShoot)
        {
            DrawTrajectory.Instance.UpdateTrajectory(forceV, rb, transform.position);
        }
    }
    
    private void OnMouseUp()
    {
        anim.SetBool("isThrow", false);
        anim.SetBool("isStandToCover", true);
        testText6.text = "mUp";
        StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<MoveToCorrectPlace>().MoveChar(coverPos));
        Debug.Log("MOUSE PHASE 3");
        DrawTrajectory.Instance.HideLine();
        DoMouseUpFuntions();
    }*/

    void DoMouseUpFuntions()
    {
        Vector3 vectorF = new Vector3();
        mouseReleasePos = Input.mousePosition;

        if (mousePressDownPos.y - mouseReleasePos.y <= 0)
        {
            Debug.Log("Küçüktür");
            vectorF = mouseReleasePos - mousePressDownPos;
        }
        else if (mousePressDownPos.y - mouseReleasePos.y > 0)
        {
            Debug.Log("Büyüktür");
            vectorF = mousePressDownPos - mouseReleasePos;
        }

        Shoot(vectorF);
    }

    public void Shoot(Vector3 Force)
    {
        if (isShoot)
            return;
        Ball ballScript = GetComponent<Ball>();
        ballScript.isUsable = false;
        rb.useGravity = true;
        rb.AddForce(new Vector3(Force.x, Force.y, Force.y) * force);
        isShoot = true;
        GameManager.Instance.NewSpawnRequest();
    }
}
