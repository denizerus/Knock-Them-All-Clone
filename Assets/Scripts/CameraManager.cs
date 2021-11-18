using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform[] views;
    public float transitionSpeed;
    Transform currentView;

    // Start is called before the first frame update
    void Start()
    {
        transitionSpeed = 5f;
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("PlayerHitBox").GetComponent<EnemyHitsPlayer>().gameContinue)
        {
            currentView = views[0];
        }
        else
        {
            currentView = views[1];
        }
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,currentView.position, Time.deltaTime * transitionSpeed);
    }
}
