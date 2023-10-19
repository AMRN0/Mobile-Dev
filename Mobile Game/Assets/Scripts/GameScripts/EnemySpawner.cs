using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    private int randomNum;
    public GameObject[] enemies;
    public int enemyCount = 5;

    public Transform spawnPosition;

    public float intervalTime = 10.0f;
    private float startTime = 0;

    public TMP_Text scoreText;

    [HideInInspector]
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetRandomNum();

        scoreText.text = "Waves Spawned: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        startTime += Time.deltaTime;

        if (startTime > intervalTime)
        {
            startTime = 0;
            Debug.Log(true);

            SpawnEnemy();

            score++;
            PlayerPrefs.SetInt("score", score);
            scoreText.text = "Waves Spawned: " + score.ToString();

        }
    }

    private void GetRandomNum()
    {
        randomNum = Random.Range(0, enemies.Length);
    }

    private float GetRandomPosition(float min, float max)
    {
        return Random.Range(min, max);
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GetRandomNum();
            Instantiate(enemies[randomNum], new Vector3(GetRandomPosition(-7f, 7f), spawnPosition.position.y, spawnPosition.position.z), Quaternion.identity);
        }
    }
}
