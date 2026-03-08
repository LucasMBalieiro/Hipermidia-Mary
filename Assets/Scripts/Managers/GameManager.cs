using System;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public float CurrentGameSpeed { get; private set; }
    
    public int HighScore { get; private set; }

    [Header("Global Speed Settings")]
    [SerializeField] private float maxSpeed = 20f;
    private float gameStartTime;
    private bool isGameRunning = false;
    
    private int currentGesture = 0;
    
    [Space(10)]
    [SerializeField] private SoundData music;
    [Space(20)]
    
    [SerializeField] private TemplateScriptableObject[] templateScriptableObjects;
    [HideInInspector] public List<GestureTemplate> gestureTemplate;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        gestureTemplate = new List<GestureTemplate>();
        InitializeGestureTemplates();
    }

    private void OnEnable()
    {
        Actions.OnStartGame += StartGameLogic;
        Actions.OnGameOver += StopGameLogic;
    }

    private void OnDisable()
    {
        Actions.OnStartGame -= StartGameLogic;
        Actions.OnGameOver -= StopGameLogic;
    }

    private void InitializeGestureTemplates()
    {
        foreach (TemplateScriptableObject template in templateScriptableObjects)
        {
            gestureTemplate.Add(PennyPincher.CreateTemplate(template.gestureName, template.points));
            template.points.Reverse();
            gestureTemplate.Add(PennyPincher.CreateTemplate(template.gestureName, template.points));
        }
    }

    public void PlayMusic() => SoundManager.Instance.CreateSound().Play(music);

    public GestureName GetRandomGesture()
    {
        int step = Random.Range(1, templateScriptableObjects.Length);
        
        currentGesture = (currentGesture + step) % templateScriptableObjects.Length;
        
        return templateScriptableObjects[currentGesture].gestureName;
    }

    public void CompareHighScore(int score)
    {
        if (score > HighScore) HighScore = score;
        Actions.OnHighscoreUpdate.Invoke();
    }

    private void StartGameLogic()
    {
        gameStartTime = Time.time;
        isGameRunning = true;
    }

    private void StopGameLogic()
    {
        isGameRunning = false;
    }

    private void Update()
    {
        if (!isGameRunning) return;

        float timeAlive = Time.time - gameStartTime;
        float calculatedSpeed = (1 + Mathf.Pow(timeAlive, 0.6f));
    
        CurrentGameSpeed = Mathf.Min(calculatedSpeed, maxSpeed);
    }
}
