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

    private SphereCollider sc;

    private Rigidbody rb;

    private float force;

    private bool isShoot;

    public Text testText4;

    public Text testText5;

    Animator anim;

    void Start()
    {
#if UNITY_ANDROID
        force = 1f;
#endif

#if UNITY_EDITOR
        force = 3f;
#endif
        coverPos = GameObject.Find("GameCharCoverPos").transform;
        throwPos = GameObject.Find("GameCharThrowPos").transform;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        testText4 = GameObject.FindGameObjectWithTag("Text4").GetComponent<Text>();
        testText5 = GameObject.FindGameObjectWithTag("Text5").GetComponent<Text>();
        rb = GetComponent<Rigidbody>();
        sc = GetComponent<SphereCollider>();
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
                mousePressDownPos = Input.mousePosition;
                anim.SetBool("isThrow", true);
                anim.SetBool("isStandToCover", false);
                StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<MoveToCorrectPlace>().MoveChar(throwPos));
                break;
            case TouchPhase.Moved:
                Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
                Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, forceInit.y)) * force;
                if (!isShoot)
                {
                    DrawTrajectory.Instance.UpdateTrajectory(forceV, rb, transform.position);
                }
                break;
            case TouchPhase.Ended:
                anim.SetBool("isThrow", false);
                anim.SetBool("isStandToCover", true);
                StartCoroutine(GameObject.FindGameObjectWithTag("Player").GetComponent<MoveToCorrectPlace>().MoveChar(coverPos));
                DrawTrajectory.Instance.HideLine();
                DoMouseUpFuntions();
                break;
        }
    }
    void DoMouseUpFuntions()
    {
        Vector3 vectorF = new Vector3();
        mouseReleasePos = Input.mousePosition;

        if (mousePressDownPos.y - mouseReleasePos.y <= 0)
        {
            vectorF = mouseReleasePos - mousePressDownPos;
        }
        else if (mousePressDownPos.y - mouseReleasePos.y > 0)
        {
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
        sc.enabled = true;
        rb.useGravity = true;
        rb.AddForce(new Vector3(Force.x, Force.y, Force.y) * force);
        isShoot = true;
        StartCoroutine(BallLifeTime(4f));
        GameManager.Instance.NewSpawnRequest();
    }

    private IEnumerator BallLifeTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}