using System;
using Audio;
using EnemyScripts;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private EnemyAnimation enemyAnimation;
    [SerializeField] private TextMeshPro textBox;
    [SerializeField] private GameObject explosionPrefab;
    
    [Space(5)]
    [SerializeField] private SoundData spawnSound;
    [SerializeField] private SoundData deathSound;
    
    private GestureName[] gestureNames;
    private int currentGesture = 0;
    private int gestureCount;
    private int scorePoints;
    private float speedMultiplier;
    
    private float playerPosition;
    
    private void OnEnable()
    {
        DrawMesh.OnGestureRecognized += HandleGestureHit;
        Actions.OnGameOver += KillEnemy;
    }

    private void OnDisable()
    {
        DrawMesh.OnGestureRecognized -= HandleGestureHit;
        Actions.OnGameOver -= KillEnemy;
    } 

    public void Initialize(EnemyScriptableObject enemySO, float playerPos)
    {
        gestureCount = Random.Range(enemySO.minRangeGesture, enemySO.maxRangeGesture +1);
        gestureNames = new GestureName[gestureCount];

        for (int i = 0; i < gestureCount; i++)
        {
            gestureNames[i] = GameManager.Instance.GetRandomGesture();
        }
        
        speedMultiplier = enemySO.speedMultiplier;
        scorePoints = enemySO.scorePoints * (int)GameManager.Instance.CurrentGameSpeed;

        playerPosition = playerPos;
        
        enemyAnimation.Initialize(enemySO);
        UpdateVisuals();
        SoundManager.Instance.CreateSound().Play(spawnSound);
    }

    private void Update()
    {
        transform.Translate(Vector2.left * ((GameManager.Instance.CurrentGameSpeed * speedMultiplier) * Time.deltaTime));

        if (transform.position.x < playerPosition)
        {
            Actions.OnGameOver.Invoke();
            KillEnemy();
        }
    }

    private void HandleGestureHit(GestureName gestureHit)
    {
        if(gestureHit == gestureNames[currentGesture]) HitEnemy();
        
    }

    private void HitEnemy()
    {
        currentGesture++;

        if (currentGesture >= gestureNames.Length)
        {
            Actions.OnScoreChange?.Invoke(scorePoints);
            KillEnemy();
        }
        else
        {
            UpdateVisuals();
        }
    }

    private void KillEnemy()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        SoundManager.Instance.CreateSound().Play(deathSound);
        
        Destroy(gameObject);
    }

    private void UpdateVisuals()
    {
        textBox.text = EnemyUtils.NameToIcon(gestureNames[currentGesture]);
    }
}
