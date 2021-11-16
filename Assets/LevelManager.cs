﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public int numOfEnemy;
    public bool isLevelCreated;
    private float newLevelRequestTime = 1f;

    private Text TextEnemy;

    // Start is called before the first frame update
    void Start()
    {
        TextEnemy = GameObject.FindGameObjectWithTag("Text4").GetComponent<Text>();
        isLevelCreated = false;
        numOfEnemy = 0;
        GenerateNewLevel();
    }

    private void GenerateNewLevel()
    {
        GameObject newCubes;
        for (int i = 0; i < 5; i++)
        {
            int randomPrefab = Random.Range(0, 4);

            if(randomPrefab != 3)
            {
                numOfEnemy++;
                newCubes = Instantiate(enemyPrefab[randomPrefab],
                    new Vector3(DetectXPosition(i),
                    enemyPrefab[randomPrefab].transform.position.y,
                    transform.position.z),
                    Quaternion.identity);
                newCubes.transform.parent = gameObject.transform;
            }
        }
        isLevelCreated = true;
    }

    private float DetectXPosition(int i)
    {
        if (i == 0)
            return -3f;
        else if (i == 1)
            return -1.5f;
        else if (i == 2)
            return 0f;
        else if (i == 3)
            return 1.5f;
        else
            return 3f;
    }

    private void Update()
    {
        TextEnemy.text = "Enemy: " + numOfEnemy.ToString();
        if (numOfEnemy == 0 && isLevelCreated)
        {
            foreach (Transform child in gameObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

            foreach(GameObject ball in balls)
            {
                if (ball.GetComponent<Ball>().isUsable == false)
                {
                    Destroy(ball);
                }
            }
            Debug.Log("LEVEL BİTTİ YENİ LEVEL!!!");
            isLevelCreated = false;
            StartCoroutine(NewLevelRequest());
        }
    }

    public IEnumerator NewLevelRequest()
    {
        yield return new WaitForSeconds(newLevelRequestTime);
        GenerateNewLevel();
    }
}