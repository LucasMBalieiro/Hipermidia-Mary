using System.Collections.Generic;
using UnityEngine;

public enum GestureName
{
    Undefined,
    HorizontalLine,
    VerticalLine,
    UpArrow,
    DownArrow,
    Square,
    Triangle
}

[CreateAssetMenu(fileName = "TemplateScriptableObject", menuName = "Scriptable Objects/Gesture")]
public class TemplateScriptableObject : ScriptableObject
{
    public GestureName gestureName;
    public List<Vector2> points;
}
