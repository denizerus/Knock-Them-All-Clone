using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform BallSpawnPoint;
    public GameObject Ball;

    void Start()
    {
        Instantiate(Ball, BallSpawnPoint);
    }
    
    void Update()
    {

    }

}
