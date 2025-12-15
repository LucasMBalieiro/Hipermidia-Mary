using System.Collections.Generic;
using Audio;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public float CurrentGameSpeed { get; private set; }

    [Header("Global Speed Settings")]
    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private float maxSpeed = 20f;
    private float gameStartTime;
    private bool isGameRunning = false;
    
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
        float calculatedSpeed = Mathf.Log(timeAlive + 1) * speedMultiplier;
    
        CurrentGameSpeed = Mathf.Min(calculatedSpeed, maxSpeed);
    }
}
