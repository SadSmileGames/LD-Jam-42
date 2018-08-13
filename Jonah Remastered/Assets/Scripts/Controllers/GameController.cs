using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public delegate void OnNextWaveDelegate();
    public static OnNextWaveDelegate OnNextWave;
    public static OnNextWaveDelegate OnNextWaveBegin;

    public GameObject[] obstacleSets;
    public Grid grid;

    public int score;
    public int highscore;

    public Wave[] waves;
    public float timeBetweenWaves;

    private int enemiesRemainingToSpawn;
    private int enemiesRemainingAlive;
    private int current = 0;
    private Wave wave;

    private float nextSpawnTime;
    private float timeBetweenWavesRemaining;

    private Transform player;

    private void OnEnable()
    {
        EnemyHealth.OnDeath += UpdateScore;
        EnemyHealth.OnDeath += UpdateRemainingEnemies;
    }

    private void OnDisable()
    {
        EnemyHealth.OnDeath -= UpdateScore;
        EnemyHealth.OnDeath -= UpdateRemainingEnemies;
    }

    public void UpdateRemainingEnemies()
    {
        enemiesRemainingAlive--;

        if (enemiesRemainingAlive == 0)
            NextWave();
    }

    public void UpdateScore()
    {
        score++;
    }

    public void NextWave()
    {
        if (OnNextWave != null)
            OnNextWave();

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        ChangeObstacles();

        player.position = grid.GetRandomNodePosition();

        if (OnNextWaveBegin != null)
            OnNextWaveBegin();

        wave = waves[Random.Range(0, waves.Length)];

        enemiesRemainingToSpawn = wave.numberOfEnemies;
        enemiesRemainingAlive = wave.numberOfEnemies;
    }

    private void ChangeObstacles()
    {
        if(wave.obstacleSet != 0)
        {
            foreach(GameObject obj in obstacleSets)
            {
                obj.SetActive(false);
            }

            obstacleSets[wave.obstacleSet - 1].SetActive(true);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        NextWave();
    }

    private void Update()
    {
        if(enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + wave.timeBetweenSpawns;

            GameObject spawnedEnemy = Instantiate(wave.enemyTypes[Random.Range(0, wave.enemyTypes.Length - 1)], grid.GetRandomNodePosition(), Quaternion.identity);
        }
    }

    [System.Serializable]
    public struct Wave
    {
        public GameObject[] enemyTypes;
        public int numberOfEnemies;
        public float timeBetweenSpawns;

        [Range(1, 3)]
        public int obstacleSet;
    }

}
