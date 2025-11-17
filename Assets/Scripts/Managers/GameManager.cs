using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TemplateScriptableObject[] templateScriptableObjects;
    
    [HideInInspector] public List<GestureTemplate> gestureTemplate;

    public static GameManager Instance { get; private set; }
    
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
    
}
