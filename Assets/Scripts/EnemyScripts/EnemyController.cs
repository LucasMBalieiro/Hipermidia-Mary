using System;
using EnemyScripts;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TextMeshPro textBox;
    
    private GestureName[] gestureNames;
    private int currentGesture = 0;
    private int scorePoints;
    private float speed;
    
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

    public void Initialize(EnemyScriptableObject enemySO, float enemySpeed, float playerPos)
    {
        gestureNames = enemySO.gestureNames;
        spriteRenderer.sprite = enemySO.sprite;
        scorePoints = enemySO.scorePoints;
        playerPosition = playerPos;
        UpdateVisuals();
        
        speed = enemySpeed;
    }

    private void Update()
    {
        transform.Translate(Vector2.left * (speed * Time.deltaTime));

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
        Destroy(gameObject);
    }

    private void UpdateVisuals()
    {
        textBox.text = EnemyUtils.NameToIcon(gestureNames[currentGesture]);
    }
}
