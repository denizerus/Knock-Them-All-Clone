using System;
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
    private float newLevelRequestTime = 1.3f;
    private Text TextEnemy;
    private Text TextLevel;
    public ParticleSystem particleConfetti;
    public GameObject winPanel;
    public GameObject losePanel;
    private Animator animatorWin;
    private Animator animatorLose;

    void Start()
    {
        particleConfetti.Stop();
        animatorWin = winPanel.GetComponent<Animator>();
        animatorLose = losePanel.GetComponent<Animator>();
        TextEnemy = GameObject.FindGameObjectWithTag("Text4").GetComponent<Text>();
        isLevelCreated = false;
        numOfEnemy = 0;
        GenerateNewLevel();

        if (!PlayerPrefs.HasKey("NumOfLevel"))
        {
            PlayerPrefs.SetInt("NumOfLevel", 1);
        }

        TextLevel = GameObject.FindGameObjectWithTag("Text5").GetComponent<Text>();
        TextLevel.text = "Level " + PlayerPrefs.GetInt("NumOfLevel").ToString();
    }

    private void Update()
    {
        TextEnemy.text = numOfEnemy.ToString();
        if (numOfEnemy == 0 && isLevelCreated)
        {
            winPanel.SetActive(true);
            GameObject.FindGameObjectWithTag("PlayerHitBox").GetComponent<EnemyHitsPlayer>().gameContinue = false;
            particleConfetti.Play();

            if (animatorWin != null)
            {
                animatorWin.SetBool("startAnim", true);
            }
            isLevelCreated = false;
            PlayerPrefs.SetInt("NumOfLevel", PlayerPrefs.GetInt("NumOfLevel") + 1);
            TextLevel.text = "Level " + PlayerPrefs.GetInt("NumOfLevel").ToString();
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (GameObject ball in balls)
            {
                if (ball.GetComponent<Ball>().isUsable == false)
                {
                    Destroy(ball);
                }
                else if (ball.GetComponent<Ball>().isUsable)
                {
                    ball.GetComponent<DragAndShoot>().enabled = false;
                }
            }
            StartCoroutine(NewLevelRequest());
        }
        else if (numOfEnemy > 0)
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (GameObject ball in balls)
            {
                if (ball.GetComponent<Ball>().isUsable)
                {
                    ball.GetComponent<DragAndShoot>().enabled = true;
                }
            }
        }
    }

    public void GenerateNewLevel()
    {
        numOfEnemy = 0;
        particleConfetti.Stop();
        GameObject.FindGameObjectWithTag("PlayerHitBox").GetComponent<EnemyHitsPlayer>().gameContinue = true;
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        GameObject.FindGameObjectWithTag("PlayerHitBox").GetComponent<EnemyHitsPlayer>().gameContinue = true;
        int checkIsItEmpty = 0;
        GameObject newCubes;

        for (int i = 0; i < 5; i++)
        {
            int randomPrefab = Random.Range(0, 4);
            if (randomPrefab != 3)
            {
                numOfEnemy++;
                newCubes = Instantiate(enemyPrefab[randomPrefab],
                    new Vector3(DetectXPosition(i),
                    enemyPrefab[randomPrefab].transform.position.y,
                    transform.position.z),
                    Quaternion.identity);
                newCubes.transform.parent = gameObject.transform;
            }
            else
            {
                checkIsItEmpty++;
            }
        }

        //IF BATTLEFIELD HAS NO ENEMY
        if (checkIsItEmpty == 5)
        {
            numOfEnemy++;
            int randomPrefab = Random.Range(0, 3);
            newCubes = Instantiate(enemyPrefab[randomPrefab],
                new Vector3(DetectXPosition(2),
                enemyPrefab[randomPrefab].transform.position.y,
                transform.position.z),
                Quaternion.identity);
            newCubes.transform.parent = gameObject.transform;
        }
        isLevelCreated = true;
    }

    public IEnumerator NewLevelRequest()
    {
        yield return new WaitForSeconds(newLevelRequestTime);
        if (animatorWin.GetBool("startAnim") == true)
        {
            animatorWin.SetBool("startAnim", false);
        }

        if (animatorLose.GetBool("startAnim") == true)
        {
            animatorLose.SetBool("startAnim", false);
        }
        DestroyLevelPrefabs();
    }

    private void DestroyLevelPrefabs()
    {
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
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
    public void RetryTheLevel()
    {
        losePanel.SetActive(true);
        if (animatorLose != null)
        {
            animatorLose.SetBool("startAnim", true);
        }
        DestroyLevelPrefabs();
    }
}
