using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform groundSpawnPoint;
    [SerializeField] private Transform airSpawnPoint;
    [SerializeField] private Transform bossSpawnPoint;
    
    [Header("Enemy Data")]
    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private EnemyScriptableObject[] enemies;
    [SerializeField] private EnemyScriptableObject bossSO;

    [Header("Spawn Data")]
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float minSpawnDelay = 1.0f;
    [SerializeField] private float maxSpawnDelay = 3.0f;
    [SerializeField] private float bossSpawnDelay = 20f;
    
    [Header("Speed Progression")]
    [SerializeField] private float maxSpeed = 20f;
    
    private Coroutine spawnCoroutine;
    private Coroutine bossSpawnCoroutine;
    

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
        spawnCoroutine = StartCoroutine(SpawnLoop());
        bossSpawnCoroutine = StartCoroutine(BossSpawnLoop());
    }

    private void GameOver()
    {
        StopCoroutine(spawnCoroutine);
        StopCoroutine(bossSpawnCoroutine);
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);
            
            int randomEnemy = Random.Range(0, enemies.Length);
            EnemyScriptableObject enemySO = enemies[randomEnemy];
            
            InstantiateEnemy(enemySO);
        }
    }

    private IEnumerator BossSpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(bossSpawnDelay);
            
            InstantiateEnemy(bossSO);
        }
    }

    private void InstantiateEnemy(EnemyScriptableObject enemySO)
    {
        float calculatedSpeed = GetSpeed();
        Transform spawnPoint = GetSpawnPoint(enemySO.enemyType);
        
        EnemyController newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        newEnemy.Initialize(enemySO, calculatedSpeed, playerPosition.position.x);
    }

    private float GetSpeed()
    {
        return Mathf.Min(GameManager.Instance.CurrentGameSpeed, maxSpeed);
    }

    private Transform GetSpawnPoint(EnemyType enemyType)
    {
        return enemyType switch
        {
            EnemyType.Ground => groundSpawnPoint,
            EnemyType.Flying => airSpawnPoint,
            EnemyType.Boss => bossSpawnPoint,
            _ => throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null)
        };
    }
}
