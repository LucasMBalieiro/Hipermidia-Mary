using UnityEngine;

public enum EnemyType
{
    Ground,
    Flying,
}

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Scriptable Objects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public GestureName[] gestureNames;
    public Sprite sprite;
    public EnemyType enemyType;
    public int scorePoints;
}
