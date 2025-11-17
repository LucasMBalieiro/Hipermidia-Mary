using UnityEngine;

[System.Serializable]
public class GestureTemplate
{
    public GestureName gestureName;
    public Vector2[] vectors;

    public GestureTemplate(GestureName gestureName, Vector2[] vectors)
    {
        this.gestureName = gestureName;
        this.vectors = vectors;
    }
}

public struct RecognitionResult
{
    public GestureName gestureName;
    public float score;

    public static readonly RecognitionResult NoMatch = new RecognitionResult { gestureName = GestureName.Undefined, score = 0 };
}