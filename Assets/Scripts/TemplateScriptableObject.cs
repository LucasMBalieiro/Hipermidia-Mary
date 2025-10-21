using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TemplateScriptableObject", menuName = "Scriptable Objects/TemplateScriptableObject")]
public class TemplateScriptableObject : ScriptableObject
{
    public List<Vector2> points;
}
