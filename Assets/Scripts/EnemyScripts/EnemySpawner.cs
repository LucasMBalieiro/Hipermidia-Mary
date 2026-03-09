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
        EnemyController newEnemy = Instantiate(enemyPrefab, new Vector3(24f, enemySO.yPosition, 0f), Quaternion.identity);
        newEnemy.Initialize(enemySO, playerPosition.position.x);
    }
}
