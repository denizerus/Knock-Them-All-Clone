using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform BallSpawnPoint;
    public GameObject Ball;
    private float newSpawnDuration = 0.5f;

    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    void Start()
    {
        SpawnNewBall();
    }
    
    void SpawnNewBall()
    {
        var ball = Instantiate(Ball, BallSpawnPoint.position, Quaternion.identity);
    }

    public void NewSpawnRequest()
    {
        Invoke("SpawnNewBall", newSpawnDuration);
    }
}
