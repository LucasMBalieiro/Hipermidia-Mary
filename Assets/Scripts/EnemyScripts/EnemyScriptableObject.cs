using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Scriptable Objects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public GestureName[] gestureNames;
    public Sprite sprite;
    public int scorePoints;
}
