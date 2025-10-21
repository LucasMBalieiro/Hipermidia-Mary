using UnityEngine;

[System.Serializable]
public class GestureTemplate
{
    public string name;
    public Vector2[] vectors;

    public GestureTemplate(string name, Vector2[] vectors)
    {
        this.name = name;
        this.vectors = vectors;
    }
}

public struct RecognitionResult
{
    public string name;
    public float score;

    public static readonly RecognitionResult NoMatch = new RecognitionResult { name = "No Match", score = 0 };
}