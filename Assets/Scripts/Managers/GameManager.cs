using System.Collections.Generic;
using Audio;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
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
    
}
