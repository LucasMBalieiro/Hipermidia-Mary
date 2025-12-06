using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public struct SpawnConfig
{
    [Header("Spawn Times")]
    public float minTime;
    public float maxTime;
    
    [Header("Spawn Speed")]
    public float spawnSpeed;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform groundSpawnPoint;
    [SerializeField] private Transform airSpawnPoint;
    
    [Header("Enemy Data")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private EnemyScriptableObject[] enemies;
    
    [Header("Spawn Data")]
    [SerializeField] private Transform playerPosition;
    [SerializeField] private SpawnConfig[] spawnConfigs;
    [SerializeField] private float difficultyChangeTime;
    
    private Coroutine spawnCoroutine;
    private int currentDifficulty;
    private int maxDifficulty;
    private float startTime;

    private void OnEnable()
    {
        Actions.OnStartGame += StartGame;
        Actions.OnGameOver += GameOver;
    }

    private void OnDisable()
    {
        Actions.OnStartGame -= StartGame;
        Actions.OnGameOver -= GameOver;
    }

    private void StartGame()
    {
        currentDifficulty = 0;
        startTime = Time.time;
        maxDifficulty = spawnConfigs.Length;
        spawnCoroutine = StartCoroutine(SpawnLoop());
    }

    private void GameOver()
    {
        StopCoroutine(spawnCoroutine);
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            float delay = Random.Range(spawnConfigs[currentDifficulty].minTime, spawnConfigs[currentDifficulty].maxTime);
            yield return new WaitForSeconds(delay);
            
            int randomSpawn = Random.Range(0, enemies.Length);
            EnemyScriptableObject enemySO = enemies[randomSpawn];

            Transform spawnPoint = enemySO.enemyType == EnemyType.Ground ? groundSpawnPoint : airSpawnPoint;
            
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
            
            enemyController.Initialize(enemies[randomSpawn], 
                spawnConfigs[currentDifficulty].spawnSpeed, playerPosition.position.x);
            
            float elapsedTime = Time.time - startTime;

            if (elapsedTime >= difficultyChangeTime && currentDifficulty < maxDifficulty -1)
            {
                currentDifficulty++;
                startTime = Time.time;
            }
        }
    }
}
